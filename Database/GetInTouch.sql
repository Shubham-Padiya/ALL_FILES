create table GetInTouch (GetInTouchID int not null primary key identity(1,1),FirstName varchar(50) not null,
			 LastName varchar(50) not null , MobileNo char(10) null , Email varchar(30) not null unique,
			 Subjects varchar(30) not null , Messages varchar(200) not null)


select * from GetInTouch