#DropDB XafEasyTestDemoEasyTest 
#Application XafEasyTestDemo 

*Action Navigation(Student) 

*CheckTable  
    RowCount = 0 

*Action New 

*FillForm 
    First Name = John 
    Last Name = Smith 

*Action Save 

*CheckFieldValues 
    Full Name = John Smith 

*Action Navigation(Student) 

*CheckTable  
    RowCount = 1 
    Columns = First Name, Last Name 
    Row = John, Smith