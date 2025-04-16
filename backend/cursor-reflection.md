# Cursor AI Prompts and Edits Documentation

## Environment Configuration Files

### 1. Creating Environment-Specific Configuration Files
**Prompt:** "create appsetting.json for UAT and dev as like staging"

**Actions Taken:**
1. Created `appsettings.UAT.json` with UAT-specific configuration:
   - Database connection string for UAT environment
   - UAT-specific server address and CORS settings

2. Created `appsettings.Development.json` with Development-specific configuration:
   - Local development database connection string
   - Development server address and CORS settings

**Key Files Created/Modified:**
- `src/AkeoIN.SuperQA.Web.Host/appsettings.UAT.json`
- `src/AkeoIN.SuperQA.Web.Host/appsettings.Development.json`

**Configuration Details:**
```json
// UAT Configuration
{
    "ConnectionStrings": {
        "Default": "server=uat-db-server;database=SuperQADb;user=uat_user;password=uat_password;Connection Timeout=60"
    },
    "App": {
        "ServerRootAddress": "http://uat.superqa.com/",
        "CorsOrigins": "http://uat.superqa.com"
    }
}

// Development Configuration
{
    "ConnectionStrings": {
        "Default": "server=localhost;database=SuperQADb;user=dev_user;password=dev_password;Connection Timeout=60"
    },
    "App": {
        "ServerRootAddress": "http://localhost:9902/",
        "CorsOrigins": "http://localhost:9902"
    }
}
```

## Database Configuration

### 1. Entity Framework Core Configuration
**File:** `src/AkeoIN.SuperQA.EntityFrameworkCore/EntityFrameworkCore/SuperQADbContextConfigurer.cs`

**Key Configuration:**
- MySQL database configuration
- Connection string handling
- Server version auto-detection

## Best Practices and Recommendations

1. **Environment Configuration:**
   - Always use environment-specific connection strings
   - Keep sensitive information secure
   - Use appropriate server addresses for each environment

2. **Database Configuration:**
   - Use auto-detection for MySQL server versions
   - Implement proper connection timeout settings
   - Maintain separate credentials for each environment

## Notes
- Replace placeholder credentials with actual environment-specific values
- Update server addresses according to actual deployment environments
- Ensure CORS settings match the intended access patterns for each environment 