INSERT INTO PAccount(Account,UserName,Password,IsActive)VALUES('admin','admin','a0f848942ce863cf53c0fa6cc684007d',1);
INSERT INTO PPermission(Name,SerialNumber,ParentSN,Level,FilePath,Icon,IsDelete) VALUES('权限管理',1,0,1,'','fa fa-link',0);
INSERT INTO PPermission(Name,SerialNumber,ParentSN,Level,FilePath,Icon,IsDelete) VALUES('角色管理',2,1,2,'','fa fa-link',0);
INSERT INTO PPermission(Name,SerialNumber,ParentSN,Level,FilePath,Icon,IsDelete) VALUES('目录管理',3,1,2,'PermissionManage.html','fa fa-link',0);