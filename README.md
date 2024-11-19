# console-vulnerabilities
CONSOLE BitDefender project to search software vulnerabilities 
Vulnerabilities:
- Application does not implement a mechanism to prevent or detect brute force attacks on login endpoint.
- The generation of token has no cryptographic signing or integrity protection, it is just Base64 encoded, it does not include an expiration mechanism, meaning it can be used indefinitely once issued and it is created using a predictable and static structure


- unpopulated .gitignore file that can lead to Sensitive Information Exposure.

NodeJS vulnerabilities:

- No content limit set for Multer (DOS if a big file is uploaded)
- Regex can be backtracked
- Username and password hardcoded
- No ENV file
- No .gitignore file

LibraryAPI

- ConnectionString in appsettings.json
- No Env file
- No .gitignore file