# ResourceAccountingSystem
Тестовое задание

## ЗАДАЧА:
Необходимо разработать систему учета ресурсов.
Состав системы:
* Сервер – WEB API
* Хранилище данных – MS SQL
* Клиент – ASP.NET Single Page

## Объекты системы
* Дом
* Адрес
* Счетчик воды
* Заводской номер
* Показания

## Функции системы
* Создать новый дом
* Удалить дом по его id
* Найти дом по его id
* Редактировать дом по его id
* Получить все дома
* Зарегистрировать новый счетчик воды в доме
* Получить дом который больше всего потребил воды
* Получить дом который меньше всего потребил воды
* Внести показания счетчика по его серийному номеру
* Внести показания счётчика по id дома

## Требования к системе
* Дом может иметь 1 счетчик воды
* Показания счетчика не могут быть отрицательными
* Дом имеет уникальный адрес

## Требования к реализации:
Выполнить обработку асинхронным методом (async await):
* Получить все дома
* Получить дом который больше всего потребил воды
* Получить дом который меньше всего потребил воды
