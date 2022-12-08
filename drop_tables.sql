-- Drop constraints
ALTER TABLE Employee DROP CONSTRAINT FK_EmployeeRoleId;
ALTER TABLE Ticket DROP CONSTRAINT FK_TicketStatusId;
ALTER TABLE Ticket DROP CONSTRAINT FK_TicketEmployeeId;

-- Drop tables
DROP TABLE Employee;
DROP TABLE Ticket;
DROP TABLE Role;
DROP TABLE TicketStatus;