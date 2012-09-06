For this database, I use SQL server 2005 express edition. In SQL server, I create a database "forum". In this database, I create four tables.


Details of tables use in database:

Table name   :  Column Name/Data type/Properties


1. user.info :  hashcode/varchar(50)/Primary key
                name/varchar(50)


2. que_list  :  que_id/int/Primary key/Identity increment=1/Identity seed=1
                title/varchar(MAX)
                que_desc/varchar(MAX)
                sub_by/varchar(50)
                sub_date/smalldatetime
                vote/tinyint/Default value=0
                spam/tinyint/Default value=0



3. ans_list  :  ans_id/int/Primary Key/Identity increment=1/Identity seed=1
                que_id/int
                ans_desc/varchar(MAX)
                sub_by/varchar(50)
                date/smalldatetime


4. vote_spam :  que_id/int
                hashcode/varchar(50)
                vote/bit
                spam/bit