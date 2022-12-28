INSERT INTO Role (RoleId, RoleName) 
VALUES (0, 'Employee'), (1, 'Manager');


INSERT INTO TicketStatus (StatusId, StatusName)
VALUES (0, 'Pending'), (1, 'Approved'), (2, 'Rejected');


INSERT INTO Employee (Email, Password, RoleId)
VALUES ('employee3@email.com', '123Pass', 0),
('employed@email.com', 'BetterPass', 0),
('manager@email.com', 'ForManager', 1);


INSERT INTO Ticket (TicketId, Reason, Amount, Description, StatusId, RequestDate, EmployeeId)
VALUES ('test1', 'Travel', 1000, 'Relocating across the galaxy', 0, GETDATE(), 1),
('test2', 'Server Cost', 100, 'Paid monthly subscription for azure', 0, GETDATE(), 1),
('test3', 'Food', 50, 'I am hungry', 0, GETDATE(), 1);
INSERT INTO Ticket (TicketId, Reason, Amount, Description, StatusId, RequestDate, EmployeeId)
VALUES ('test4', 'Travel', 500, 'To the next state', 0, GETDATE(), 2),
('test5', 'Hardware Cost', 350, 'Paid for azure computer', 0, GETDATE(), 2),
('test6', 'Food', 30, 'I am hungry', 0, GETDATE(), 2);
INSERT INTO Ticket (TicketId, Reason, Amount, Description, StatusId, RequestDate, EmployeeId)
VALUES ('test7', 'Travel', 2000, 'Going to Arkansas', 0, GETDATE(), 3),
('test8', 'Personal Cost', 100, 'Someone bummed 100 bucks off me', 0, GETDATE(), 3),
('test9', 'Food', 20, 'I am hungry', 0, GETDATE(), 3);