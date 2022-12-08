INSERT INTO Role (RoleId, RoleName) 
VALUES (0, 'Employee'), (1, 'Manager');


INSERT INTO TicketStatus (StatusId, StatusName)
VALUES (0, 'Pending'), (1, 'Approved'), (2, 'Rejected');


INSERT INTO Employee (Email, Password, RoleId)
VALUES ('employee3@email.com', '123Pass', 0),
('employed@email.com', 'BetterPass', 0),
('manager@email.com', 'ForManager', 1);


INSERT INTO Ticket (Reason, Amount, Description, StatusId, EmployeeId)
VALUES ('Travel', 1000, 'Relocating across the galaxy', 0, 1),
('Server Cost', 100, 'Paid monthly subscription for azure', 0, 1),
('Food', 50, 'I am hungry', 0, 1);
INSERT INTO Ticket (Reason, Amount, Description, StatusId, EmployeeId)
VALUES ('Travel', 500, 'To the next state', 0, 2),
('Hardware Cost', 350, 'Paid for azure computer', 0, 2),
('Food', 30, 'I am hungry', 0, 2);
INSERT INTO Ticket (Reason, Amount, Description, StatusId, EmployeeId)
VALUES ('Travel', 2000, 'Going to Arkansas', 0, 3),
('Personal Cost', 100, 'Someone bummed 100 bucks off me', 0, 3),
('Food', 20, 'I am hungry', 0, 3);