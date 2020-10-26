delete from dbo.AspNetRoles;
insert into dbo.AspNetRoles (Id,[Name],NormalizedName,Title,Seq) values ('A0EE3785-9481-4DA2-BBC3-81CEDBDE93EF','Admin','ADMIN','Administrator',30);
insert into dbo.AspNetRoles (Id,[Name],NormalizedName,Title,Seq) values ('A72A1CCD-C80C-46BF-AA51-C9122F08AEAA','User','USER','User',20);
insert into dbo.AspNetRoles (Id,[Name],NormalizedName,Title,Seq) values ('DFB9897F-D047-4607-A0CA-FB1AA86DE2D1','Guest','GUEST','Guest',10);

delete from dbo.AspNetUsers;
insert into dbo.AspNetUsers (id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,
								SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled, LockoutEnd,LockoutEnabled,AccessFailedCount,Title) 
								values ('4F9415DE-E69F-4412-B508-4C20C620DDDE','Admin','ADMIN','no email','NO EMAIL',1,'AQAAAAEAACcQAAAAEOn1si4RHnIcKIiisyWetWE4uYxV6eZ0KZspSBFuoFph+OZJYCFW1ReR6uBfm1YN4g==',
								'F2POTDITTIO6AJHBL5Z7PA6USV7DYE3I','47524004-0a26-4315-a111-7b61f6138031',null,0,0,null,1,0,'Administarator');

delete from dbo.AspNetUserRoles;
insert into dbo.AspNetUserRoles (UserId,RoleId) values('4F9415DE-E69F-4412-B508-4C20C620DDDE','A0EE3785-9481-4DA2-BBC3-81CEDBDE93EF');