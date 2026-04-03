# AuditLog Endpoint Fix - Invalid UserId FK Error

## Steps:
- [x] 1. Create TODO.md
- [ ] 2. Update AuditLogRepository.cs: Add User exists check + DbUpdateException handling
- [ ] 3. Rebuild and test with valid UserId
- [ ] 4. Verify DB has users: SELECT TOP 5 UserId, Name FROM [dbo].[User];
- [ ] 5. Update TODO.md and complete

**Next**: User must insert a test user or use existing UserId, then endpoint works.
