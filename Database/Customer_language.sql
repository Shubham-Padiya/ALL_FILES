create table Customer_language (UserID int not null foreign key(UserID) references DetailsOfUser(UserID),
			 Languages varchar(50) not null)

select * from Customer_language