create table ServiceProviderRating(SPID int not null foreign key(SPID) references DetailsOfUser(UserID),
			 SPRating decimal not null)

select * from ServiceProviderRating