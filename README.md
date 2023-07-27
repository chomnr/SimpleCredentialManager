# SimpleCredentialManager
----------------------------------------------------------------

View Preview<br>
[https://www.youtube.com/watch?v=MOxt6Icv19Q
](https://www.youtube.com/watch?v=ZM_laTrbEWw)


This is a simple credential manager written in C#. The credentials are encrypted with AES 256, so I can assure you that if anybody gains access to your .scm file, they will not be able to get through it without the key file. I wrote it in a modular way so that you can add and remove features to your liking. With a little bit of tweaking, you can easily incorporate your encryption method. It was a fun little 3-day project, and the purpose of this project was to become familiar with incorporating AES in projects.

## Getting Started
1. Install [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

## Tutorial
### 1. Running the .exe
```
You can simply compile the program yourself OR going into releases and download the .EXE
```

### 2. NONE Window
```
  ______  ______  __    __
 /\  ___\/\  ___\/\ "-./  \
 \ \___  \ \ \___\ \ \-./\ \
  \/\_____\ \_____\ \_\ \ \_\
   \/_____/\/_____/\/_/  \/_/ v1.0.0

    [0] Create new key & store
    [1] Import existing key & store

> Enter '0' to create a new key & store.
> Enter '1' to import an existing key & store ( a dialog will show up to simplify the process )
```

### 3. Decryption Window
If everything goes well you be redirected to this screen
```
Decrypted contents successfully.
```

If not you will be displayed the error then this screen will show up.
```
Failed to decrypt contents.
```
### 4. MODIFY Window
After importing or creating the key and store you be redirected to this screen.
```
  ______  ______  __    __
 /\  ___\/\  ___\/\ "-./  \
 \ \___  \ \ \___\ \ \-./\ \
  \/\_____\ \_____\ \_\ \ \_\
   \/_____/\/_____/\/_/  \/_/ v1.0.0

    [0] Create a new credential
    [1] Read a credential
    [2] Update a credential
    [3] Delete a credential

>
```
anything from here on out is self-explantory.
