create table TypeOfRole (RoleID int identity(1,1) not null primary key , RoleName varchar(50) not null)
select * from TypeOfRole

insert into TypeOfRole (RoleID,RoleName) values(1,'Admin')

insert into TypeOfRole values('Customer')
insert into TypeOfRole values('Service Provider')

delete from TypeOfRole where RoleID=1

SET identity_insert TypeOfRole on