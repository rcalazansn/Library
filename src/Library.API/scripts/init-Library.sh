#!/bin/bash
wait_time=15s
password=1q2w3e4r@DO

# wait for SQL Server to come up
echo importing data will start in $wait_time...
sleep $wait_time
echo executing script...

# run the init script to create the DB and the tables in /table
/opt/mssql-tools/bin/sqlcmd -S library-api-db -U sa -P $password -i /tmp/00-Library.sql
/opt/mssql-tools/bin/sqlcmd -S library-api-db -U sa -P $password -i /tmp/01-Library.sql