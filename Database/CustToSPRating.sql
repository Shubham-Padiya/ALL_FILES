create table CustToSPRating(CustomerID int not null foreign key(CustomerID) references DetailsOfUser(UserID),
			 SPID int not null foreign key(SPID) references DetailsOfUser(UserID),
			 OnTimeArrival int not null,Friendly int not null,Quality int not null,Feedback varchar(100) null,
			 Rating decimal not null)

select * from CustToSPRating