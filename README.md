# console-vulnerabilities
CONSOLE BitDefender project to search software vulnerabilities 
Vulnerabilities:
- Application does not implement a mechanism to prevent or detect brute force attacks on login endpoint.
- The generation of token has no cryptographic signing or integrity protection, it is just Base64 encoded, it does not include an expiration mechanism, meaning it can be used indefinitely once issued and it is created using a predictable and static structure
- The application ads infinite loops by loop referencing class objects (LoopManager -> LoopBll -> LoopCode)
- The application ads infinite loops through dependency injection, (InjectionLoopManager -> InjectionLoopBll)
- The application ads memory leaks: 
	- Leaky List
	- Port leaks by keeping them open
	- Timer leaks for objects that are attached to timer events
	- Pub/Sub not removing subscribers
	- resource exhaustion
		- DB connection leaks for not closing opened DB connections
		- unmanaged threads not being closed
		- thread dead-locks
		- GC pressure 
		- memory leaks through Pub/Sub
- The Application is using unsecure encryption
	- Short RSA keys (512 bit)
	- Using default hash algorithm deemed unsecure
	- having the private key written in the code
- The appllication has unsecured JWT tokens
	- Secret is exposed in code
	- Token has no expiration date and can be used any Timer
- The application is exposed to SQL injection
- The application is exposed to resource exhaustion due to not closing opened files
- The application is exposed to insecure deserialization
- The application has an unpopulated .gitignore file that can lead to Sensitive Information Exposure.


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