create database ElectricalDevices_v3;

use ElectricalDevices_v3;

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

create table orders
(
order_id int not null primary key identity(1,1),
order_date date not null,
shiping_date date not null
);

create table suppliers
(
supplier_id int not null primary key identity(1,1),
suplier_name nvarchar(50) not null
);

create table DeviceModels
(
deviceModel_id int not null primary key identity(1,1),
model_name nvarchar(50) not null,
model_type nvarchar(50) not null,
[weight] int not null,
price money not null,
stock_balance int,
manufacturer_FK int foreign key(manufacturer_FK) references manufacturers(manufacturer_id),
supplier_FK int foreign key(supplier_FK) references suppliers(supplier_id)
);

create table devices
(
device_id int not null primary key identity(1,1),
deviceModel_FK int foreign key(deviceModel_FK) references DeviceModels(deviceModel_id),
serial_number nvarchar(50) not null,
manufacture_date date not null,
isDefected bit,
order_FK int foreign key(order_FK) references orders(order_id)
);

create table deviceOrder
(
deviceModel_id int,
order_id int,
primary key(deviceModel_id,order_id), 
foreign key(deviceModel_id) references DeviceModels(deviceModel_id),
foreign key(order_id) references orders(order_id),
amount int
);
--=====================================================================================================
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

insert into DeviceModels
values
-- model_name, model_type, weight, price, stockBalance, manufacturer_FK, supplier
('TWK 7603', '�������������', 900, 2699, 2, 3, 1), --bosch
('TWK 3P420', '�������������', 1200, 3599, 1, 3, 1),--bosch
('AR3119', '����', 650, 1399, 3, 4, 2),--aresa
('RL-C284', '����', 1100, 1799, 2, 1, 3),--redmond
('BGBS2LB1', '�������', 3500, 6999, 1, 3, 1);--bosch

insert into orders
values
--order_date,shiping_date
('2023-01-21','2023-01-22'),
('2023-01-23','2023-01-23');


insert into devices
values
--deviceModel_FK,serial_number,manufacture_date,isDefected,order_FK
(1,'BOSCH34-7712', '2022-11-22',0,1),
(1,'BOSCH34-7714', '2022-11-22',0,null),
(2,'BOSCH35-1225', '2022-12-07',0,1),
(3,'ARESA11-0044', '2022-03-12',0,null),
(3,'ARESA11-0048', '2022-03-13',0,null),
(3,'ARESA11-0054', '2022-03-14',0,null),
(4,'RED114-0777', '2021-03-12',0,2),
(4,'RED114-0677', '2021-01-02',0,null),
(5,'BOSCH22-0716', '2022-06-23',0,null);


insert into deviceOrder
values
--deviceModel_id,order_id,amount
(1,1,1),--������ �����
(2,1,1),--������ �����
(4,2,1);--������ �����

--=====================================================================================================

select * from countries;
select * from manufacturers;
select * from suppliers;
select * from DeviceModels;
select * from orders;
select * from devices;
select * from deviceOrder;

--=====================================================================================================
--����� ����� ������� ��� ��������
select TOP 1 model_name, model_type, price from DeviceModels
order by price desc;

--����� ������� � ����� � �������� ��������
select model_name, model_type, price from DeviceModels
where price >= 1000 
and price <= 3000;

--����� ��� ������� ��������� �������������
select model_name, model_type, manufacturer_name from DeviceModels
inner join manufacturers on manufacturer_id = manufacturer_FK
where manufacturer_name = 'Bosch';

--����� ��� ������� � �������� ����� �������
select  model_name, model_type, serial_number, manufacture_date from devices
inner join DeviceModels on deviceModel_FK = DeviceModels.deviceModel_id
where manufacture_date = '2022-06-23';

--����� ��� �������, ��� ��� ��������� � �������� ��������(��������) ��� ��������� ������������� � � �����
select model_name, model_type,[weight], manufacturer_name from DeviceModels
inner join manufacturers on manufacturer_id = manufacturer_FK
where [weight]>=1000
and [weight]<=4000
and manufacturer_name = 'Bosch';

--����� ���� �������� ��������� �� ������������ ������ �� ������ ������� ������
select (sum(amount)*100/(select sum(amount) as AllSold from deviceOrder)) from deviceOrder as DevOrd
inner join orders on Orders.order_id = DevOrd.order_id
where shiping_date >= '2023-01-20' and shiping_date <= '2023-01-22';

--����� ����� ���������� ������(������� ���������� ����������)
select TOP 1 model_name, model_type, sum(amount) as NumSold from deviceOrder as DevOrd
inner join DeviceModels on DevOrd.deviceModel_id = DeviceModels.deviceModel_id
group by model_name, model_type 
order by NumSold desc;

--����� ���� ������� ��������(��� ��������� ������ ��������), ����������� �� ��������� ����������
select (sum(stock_balance)*100/(select sum(stock_balance) from DeviceModels
								inner join suppliers on DeviceModels.supplier_FK = suppliers.supplier_id
								where suppliers.suplier_name = 'Supplier1')) 
								as ShareOfCheapGoods
from DeviceModels
inner join suppliers on DeviceModels.supplier_FK = suppliers.supplier_id
where DeviceModels.price < 3700
and suppliers.suplier_name = 'Supplier1';

--����� ���������� ����������� ��������, ����������� �� �������� ������ ��� ��������� ����������
select count(isDefected) from devices
inner join DeviceModels on devices.deviceModel_FK = DeviceModels.deviceModel_id
inner join manufacturers on DeviceModels.manufacturer_FK = manufacturers.manufacturer_id
inner join suppliers on DeviceModels.supplier_FK = suppliers.supplier_id
where isDefected = 1
and manufacturers.manufacturer_name = 'Bosch'
and suppliers.suplier_name = 'Supplier1';

--����� ������� ��������� �������� ��������� �� ������������ ���������� �������
select avg(price) as AVGPrice from DeviceModels
inner join deviceOrder as DevOrd on DevOrd.deviceModel_id = DeviceModels.deviceModel_id 
inner join Orders on Orders.order_id = DevOrd.order_id
where shiping_date >= '2023-01-20' and shiping_date <= '2023-01-22'; 

--����� �� ������ ��� ������� ��� ��������� ����, ��� ������� ��������� �������� ��������� �������������
select model_name, model_type, manufacturers.manufacturer_name, price from DeviceModels
inner join manufacturers on DeviceModels.manufacturer_FK = manufacturers.manufacturer_id
where manufacturers.manufacturer_name = 'Bosch'
group by price, model_name, model_type, manufacturers.manufacturer_name
having price > (select avg(price) from DeviceModels
				inner join manufacturers on DeviceModels.manufacturer_FK = manufacturers.manufacturer_id
				where manufacturers.manufacturer_name = 'Bosch');