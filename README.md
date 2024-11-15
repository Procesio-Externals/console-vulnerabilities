# console-vulnerabilities
CONSOLE BitDefender project to search software vulnerabilities 
Vulnerabilities:
- Application does not implement a mechanism to prevent or detect brute force attacks on login endpoint.
- The generation of token has no cryptographic signing or integrity protection, it is just Base64 encoded, it does not include an expiration mechanism, meaning it can be used indefinitely once issued and it is created using a predictable and static structure


- unpopulated .gitignore file
