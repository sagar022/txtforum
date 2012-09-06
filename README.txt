Welcome to @txtforum, @txtforum is a forum on SMS where you can ask or give answers of any type of technical/non-technical questions.


This project is developed in C# (ASP.NET) and for database I use SQL Server 2005 express edition.

*******

How to run this project ?

1. Download and install Visual Studio 2010 (.Net framework 4.0) or later version.

2. Download and install SQL server 2005 express edition. You can download the database of this project from this repo in folder "@txtforum database". Attach this database through SQL server.

3. Now, open this project in Visual Studio 2010. Change your "connection string" in Blogic.cs file in App_Code folder. Set your connection string to 

" server=<your server name>;user id=<your user id>;password=<your password>;initial catalog=forum;MultipleActiveResultSets=true "

5. Now, you are ready, run this project.

Note: For database details, read "README_DATABASE.TXT".

******

In this project, we are using six keywords, which refers respective .aspx page.

@txtforum         =>>  index.aspx             (Home page)
@txtforum.login   =>>  login.aspx             (Login page)
@txtforum.que     =>>  get_que_list.aspx      (For getting recent questions list)
@txtforum.ask     =>>  new_que.aspx           (For ask new question)
@txtforum.my.que  =>>  get_quelist_me.aspx    (Getting recent questions asked by user)
@txtforum.my.ans  =>>  quelist_myans.aspx     (Getting recent questions list in which user gave the answers)