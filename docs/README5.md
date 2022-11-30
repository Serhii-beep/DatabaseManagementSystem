# REST web-сервіси. Реалізація клієнтського проекту за OpenAPI Specification
Для генерації клієнту, потрібен файл з OpenAPI специфікацією
![Alt text](../img/SwagerJson.png?raw=true)
Файл має наступний вигляд
<br>
![Alt text](../img/SwagerFile.png?raw=true)
<br>
У VisualStudio підключаємо web сервіс, куди передаємо посилання на цей файл. Далі автоматично генерується код клієнта, який має наступний вигляд.
![Alt text](../img/GeneratedClient.png?raw=true)
Нижче наведено приклад його використання в консольній програмі
![Alt text](../img/GeneratedClientMain.png?raw=true)
<br>
![Alt text](../img/GeneratedClientExec.png?raw=true)

