create table SPMoreDetails(UserID int not null foreign key(UserID) references DetailsOfUser(UserID),
			 Nationality varchar(20) null,Gender varchar(10) not null, AccountStatus bit not null)


select * from SPMoreDetails