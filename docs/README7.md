# Варіант проекту із використанням MS Sql Server
Додаємо моделі, які майже ідентичні до минулих сутностей, та DbContext
```c#
    public class DBMSDbContext : DbContext
    {
        public virtual DbSet<Database> Databases { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public  virtual DbSet<Row> Rows { get; set; }
        public virtual DbSet<Field> Fields { get; set; }

        public DBMSDbContext(DbContextOptions<DBMSDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Database>(entity =>
            {
                entity.HasMany(d => d.Tables)
                    .WithOne(t => t.Database)
                    .HasForeignKey(t => t.DatabaseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Table>(entity =>
            {
                entity.HasMany(t => t.Fields)
                    .WithOne(f => f.Table)
                    .HasForeignKey(f => f.TableId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(t => t.Rows)
                    .WithOne(r => r.Table)
                    .HasForeignKey(r => r.TableId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
```
Задаємо рядок підключення та нову реалізацію інтерфейсу IDatabaseFileManager, яка використовуватиме Sql Server
```c#
public class SqlServerManager : IDatabaseFileManager
    {
        private readonly DBMSDbContext _context;

        public SqlServerManager(DBMSDbContext context)
        {
            _context = context;
        }

        public void DeleteDatabase(string databaseName)
        {
            var db = _context.Databases.FirstOrDefault(x => x.Name == databaseName);
            _context.Databases.Remove(db);
            _context.SaveChanges();
        }

        public List<string> GetAllDatabaseNames()
        {
            return _context.Databases.Select(db => db.Name).ToList();
        }

        public Database LoadDatabase(string databaseName)
        {
            var db = _context.Databases.Where(db => db.Name == databaseName).Include(db => db.Tables).ThenInclude(t => t.Fields).FirstOrDefault();
            if(db == null)
            {
                throw new Exception($"No database with name = ${databaseName}");
            }
            Database result = new Database(db.Name);
            result.Tables = new System.Collections.ObjectModel.ObservableCollection<DatabaseEntities.Table>();
            for(int i = 0; i < db.Tables.Count; ++i)
            {
                result.Tables.Add(new DatabaseEntities.Table(db.Tables.ElementAt(i).Name));
                for(int j= 0; j < db.Tables.ElementAt(i).Fields.Count; ++j)
                {
                    Fields.Field field;
                    var currField = db.Tables.ElementAt(i).Fields.ElementAt(j);
                    switch(currField.Type)
                    {
                        case "Char":
                            field = new CharField(currField.Name);
                            break;
                        case "Integer":
                            field = new IntegerField(currField.Name);
                            break;
                        case "Money":
                            field = new MoneyField(currField.Name);
                            break;
                        case "MoneyInterval":
                            field = new MoneyIntervalField(currField.Name);
                            break;
                        case "Real":
                            field = new RealField(currField.Name);
                            break;
                        case "String":
                            field = new StringField(currField.Name);
                            break;
                        default:
                            field = new IntegerField("Id");
                            break;
                    }
                    result.Tables[i].AddField(field);
                }
                var rows = _context.Rows.Where(r => r.TableId == db.Tables.ElementAt(i).Id).ToList();
                for(int j = 0; j < rows.Count; ++j)
                {
                    List<string> values = rows[j].Values.Split(',').Select(x => x = x.Trim()).ToList();
                    result.Tables[i].UpdateRow(values, j);
                }
            }
            return result;
        }

        public void SaveDatabase(Database database)
        {
            WebApi.Models.Database db = new WebApi.Models.Database();
            db.Name = database.Name;
            for(int i = 0; i < database.Tables.Count; ++i)
            {
                WebApi.Models.Table t = new WebApi.Models.Table();
                t.Name = database.Tables[i].Name;
                for(int j = 0; j < database.Tables[i].Fields.Count; ++j)
                {
                    WebApi.Models.Field f = new WebApi.Models.Field();
                    Fields.Field currField = database.Tables[i].Fields[j];
                    f.Name = currField.Name;
                    f.Type = currField.Type;
                    f.DefaultValue = currField.DefaultValue;
                    t.Fields.Add(f);
                }
                for(int j = 0; j < database.Tables[i].Rows.Count; ++j)
                {
                    WebApi.Models.Row r = new WebApi.Models.Row();
                    r.Values = string.Join(',', database.Tables[i].Rows[j].Values);
                    t.Rows.Add(r);
                }
                db.Tables.Add(t);
            }
            _context.Databases.Add(db);
            _context.SaveChanges();
        }
    }
```
Тепер для того щоб змінити місце зберігання даних (у файлі чи в БД) достатньо змінити реалізацію інтерфейсу IDatabaseFileManager в DI контейнері. Оскільки контролер використовує інтерфейс, то в ньому міняти ніочго не потрібно
