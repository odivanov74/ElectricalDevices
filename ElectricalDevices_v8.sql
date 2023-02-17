create database ElectricalDevices;

use ElectricalDevices;

create table countries
(
country_id int not null primary key identity(1,1),
country_name nvarchar(50) not null
);

create table manufacturers
(
manufacturer_id int not null primary key identity(1,1),
manufacturer_name nvarchar(50) not null,
country_FK int foreign key(country_FK) references countries(country_id)
);

create table suppliers
(
supplier_id int not null primary key identity(1,1),
supplier_name nvarchar(50) not null
);

create table [Types]
(
Type_id int not null primary key identity(1,1),
Type_name nvarchar(50) not null
);

create table Models
(
model_id int not null primary key identity(1,1),
model_name nvarchar(50) not null,
type_FK int foreign key(type_FK) references [types](Type_id),
[weight] int not null,
price money not null,
stock_balance int,
manufacturer_FK int foreign key(manufacturer_FK) references manufacturers(manufacturer_id),
supplier_FK int foreign key(supplier_FK) references suppliers(supplier_id),
reserved int
);

--alter table models
--add reserved int;


create table devices
(
device_id int not null primary key identity(1,1),
Model_FK int foreign key(Model_FK) references Models(Model_id),
serial_number nvarchar(50) not null,
manufacture_date date not null,
order_FK int foreign key(order_FK) references orders(order_id),
basket_FK int foreign key(basket_FK) references baskets(basket_id),
isDefected bit
);

create table users
(
user_id int not null primary key identity(1,1),
user_login nvarchar(50) not null,
user_password nvarchar(50) not null,
--salt nvarchar(50) not null,
role nvarchar(50) not null
);

create table Clients
(
client_id int not null primary key identity(1,1),
client_name nvarchar(50) not null,
phone nvarchar(50) not null,
personal_discount int,
user_FK int foreign key(user_FK) references users(user_id)
);

create table orders
(
order_id int not null primary key identity(1,1),
order_name nvarchar(50) not null,
order_date date not null,
client_FK int foreign key(client_FK) references clients(client_id)
);

create table modelOrder
(
model_id int,
order_id int,
primary key(model_id,order_id), 
foreign key(model_id) references Models(model_id),
foreign key(order_id) references orders(order_id),
amount int
);

create table baskets
(
basket_id int not null primary key identity(1,1),
basket_name nvarchar(50) not null,
client_FK int foreign key(client_FK) references clients(client_id)
);

create table modelBasket
(
model_id int,
basket_id int,
primary key(model_id,basket_id), 
foreign key(model_id) references Models(model_id),
foreign key(basket_id) references baskets(basket_id),
amount int,
inStock bit
);


--=====================================================================================================
insert into baskets
values
--basket_name,client_FK
('Электротовары', 1);


insert into users
values
--user_login, user_password, role
('Admin', 'Admin', 'administrator'),
('Ivanov', 'pass1', 'client');

insert into clients
values
--client_name, phone, personal_discount,user_FK
('Иванов Иван', '+7(913)777-77-77', 0, 2);

insert into countries
values
('Russian Federation'),
('Belarus'),
('Germany'),
('United Kingdom'),
('China'),
('Italy');

insert into manufacturers
values
--manufacturer_name,country_FK
('Redmond',1),
('Vitek',1),
('Bosch',3),
('Aresa',2),
('Atlant',2),
('Dyson',4),
('Kenwood',4),
('Xiaomi',5),
('AEG',6),
('Elica',6),
('Haier',5);

insert into suppliers
values
('Supplier1'),
('Supplier2'),
('Supplier3'),
('Supplier4'),
('Supplier5'),
('Supplier6');

insert into [Types]
values
('Электрочайник'),
('Утюг'),
('Пылесос');

insert into models
values
-- model_name, Type_FK, weight, price, stockBalance, manufacturer_FK, supplier_FK, reserved
('TWK 7603', 1, 900, 2699, 2, 3, 1, 0), --bosch
('TWK 3P420', 1, 1200, 3599, 1, 3, 1, 0),--bosch
('AR3119', 2, 650, 1399, 3, 4, 2, 0),--aresa
('RL-C284', 2, 1100, 1799, 1, 1, 3, 0),--redmond
('BGBS2LB1', 3, 3500, 6999, 1, 3, 1, 0),--bosch
('TWK 7607', 1, 950, 2799, 5, 3, 1, 0); --bosch

insert into orders
values
--order_name, order_date, client_FK
('ORDER-1','2023-01-21',1),
('ORDER-2','2023-01-23',1);


insert into devices
values
--Model_FK, serial_number, manufacture_date, order_FK, basket_FK, isDefected
(1,'BOSCH34-7712', '2022-11-22',1,null,0),
(1,'BOSCH34-7714', '2022-11-22',1,null,0),
(2,'BOSCH35-1225', '2022-12-07',null,null,0),
(3,'ARESA11-0044', '2022-03-12',null,null,0),
(3,'ARESA11-0048', '2022-03-13',null,null,0),
(3,'ARESA11-0054', '2022-03-14',null,null,0),
(4,'RED114-0777', '2021-03-12',null,null,0),
(4,'RED114-0677', '2021-01-02',2,null,0),
(5,'BOSCH22-0716', '2022-06-23',null,null,0);


insert into modelOrder
values
--model_id,order_id,amount
(1,1,1),--первый заказ
(2,1,1),--первый заказ
(4,2,1);--второй заказ

--=====================================================================================================

delete from countries;
delete from manufacturers;
delete from suppliers;
delete from [types];
delete from models;
delete from orders;
delete from devices;
delete from modelOrder;
delete from users;
delete from clients;
delete from modelbasket;
delete from baskets;

--delete from users where user_id = 19;
--delete from clients where client_id = 5;
--delete from orders where order_id = 8;
--update modelbasket set amount = 0 where model_id = 1 and basket_id = 1;
--delete from modelBasket where model_id = 1;
--delete from baskets where basket_id = 10;

----=====================================================================================================
----вывести все права указанного пользователя
--select rights.right_id, right_name from rights
--inner join userRight as UR on rights.right_id = UR.right_id 
--inner join users on users.user_id = UR.user_id
--where users.user_name = 'Owner';


----найти самый дорогой вид приборов
--select TOP 1 model_name, modelType_name, price from DeviceModels
--inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
--order by price desc;

----найти приборы с ценой в заданных пределах
--select model_name, modelType_name, price from DeviceModels
--inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
--where price >= 1000 
--and price <= 3000;

----найти все приборы заданного производителя
--select model_name, modelType_name, manufacturer_name from DeviceModels
--inner join manufacturers on manufacturer_id = manufacturer_FK
--inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
--where manufacturer_name = 'Bosch';

----найти все приборы с заданной датой выпуска
--select  model_name, modelType_name, serial_number, manufacture_date from devices
--inner join DeviceModels on deviceModel_FK = DeviceModels.deviceModel_id
--inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
--where manufacture_date = '2022-06-23';

----найти все приборы, чей вес находится в заданных пределах(интервал) для заданного производителя и в целом
--select model_name, modelType_name, [weight], manufacturer_name from DeviceModels
--inner join modelTypes on modelTypes.modelType_id = modelType_FK
--inner join manufacturers on manufacturer_id = manufacturer_FK
--where [weight]>=1000
--and [weight]<=4000
--and manufacturer_name = 'Bosch';

----найти долю приборов проданных за определенный период от общего времени продаж
--select (sum(amount)*100/(select sum(amount) as AllSold from deviceModelOrder)) from deviceModelOrder as DevOrd
--inner join orders on Orders.order_id = DevOrd.order_id
--where shiping_date >= '2023-01-20' and shiping_date <= '2023-01-22';

----найти самый популярный прибор(продано наибольшее количество)
--select TOP 1 model_name, modelType_name, sum(amount) as NumSold from deviceModelOrder as DevOrd
--inner join DeviceModels on DevOrd.deviceModel_id = DeviceModels.deviceModel_id
--inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
--group by model_name, modelType_name 
--order by NumSold desc;

----найти долю дешевых приборов(чья стоимость меньше заданной), поступивших от заданного поставщика
--select (sum(stock_balance)*100/(select sum(stock_balance) from DeviceModels
--								inner join suppliers on DeviceModels.supplier_FK = suppliers.supplier_id
--								where suppliers.supplier_name = 'Supplier1')) 
--								as ShareOfCheapGoods
--from DeviceModels
--inner join suppliers on DeviceModels.supplier_FK = suppliers.supplier_id
--where DeviceModels.price < 3700
--and suppliers.supplier_name = 'Supplier1';

----найти количество бракованных приборов, поступивших из заданной страны для заданного поставщика
--select count(isDefected) from devices
--inner join DeviceModels on devices.deviceModel_FK = DeviceModels.deviceModel_id
--inner join manufacturers on DeviceModels.manufacturer_FK = manufacturers.manufacturer_id
--inner join suppliers on DeviceModels.supplier_FK = suppliers.supplier_id
--where isDefected = 1
--and manufacturers.manufacturer_name = 'Bosch'
--and suppliers.supplier_name = 'Supplier1';

----найти среднюю стоимость приборов проданных за определенный промежуток времени
--select avg(price) as AVGPrice from DeviceModels
--inner join deviceModelOrder as DevOrd on DevOrd.deviceModel_id = DeviceModels.deviceModel_id 
--inner join Orders on Orders.order_id = DevOrd.order_id
--where shiping_date >= '2023-01-20' and shiping_date <= '2023-01-22'; 

----найти на складе все приборы чья стоимость выше, чем средняя стоимость приборов заданного производителя
--select model_name, modelType_name, manufacturers.manufacturer_name, price from DeviceModels
--inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
--inner join manufacturers on DeviceModels.manufacturer_FK = manufacturers.manufacturer_id
--where manufacturers.manufacturer_name = 'Bosch'
--group by price, model_name, modelType_name, manufacturers.manufacturer_name
--having price > (select avg(price) from DeviceModels
--				inner join manufacturers on DeviceModels.manufacturer_FK = manufacturers.manufacturer_id
--				where manufacturers.manufacturer_name = 'Bosch');

--Select deviceModel_id, model_name, modelType_name, weight, price, stock_balance, manufacturer_name, supplier_name  from deviceModels
--inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
--inner join manufacturers on manufacturers.manufacturer_id = manufacturer_FK
--inner join suppliers on suppliers.supplier_id = supplier_FK;

--select device_id, model_name, modelType_name, serial_number, manufacture_date, isDefected, order_name from devices
--inner join Orders on Orders.order_id = devices.order_FK
--inner join DeviceModels on devices.deviceModel_FK = DeviceModels.deviceModel_id
--inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK

--select device_id, model_name, modelType_name, serial_number, manufacture_date, isDefected, order_FK from devices
--inner join DeviceModels on devices.deviceModel_FK = DeviceModels.deviceModel_id
--inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK

--select manufacturer_id, manufacturer_name, country_name from manufacturers
--inner join countries on country_FK = countries.country_id;