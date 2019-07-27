# Console Encryption


## Changes for Run the project:

For run the project you must be change de name of the folder.
1. git clone @githubProject -- after you clone the project or download the name of the Folder is "EncryptBlazor"
2. change the name of the folder "EncryptBlazor" to "ConsoleEncryption"
3. then restore the project, by console, "dotnet restore"
4. then "dotnet run" :)

### Path of Files.

#### wwwrot/index.js

```javascript
const readUploadedFileAsText = (inputFile) => {
    const temporaryFileReader = new FileReader();
    return new Promise((resolve, reject) => {
        temporaryFileReader.onerror = () => {
            temporaryFileReader.abort();
            reject(new DOMException("Problem parsing input file."));
        };
        temporaryFileReader.addEventListener("load", function () {
            console.log("JS : file read done");
            resolve(temporaryFileReader.result.split(',')[1]);
        }, false);
        temporaryFileReader.readAsDataURL(inputFile.files[0]);
    });
};
getFileData = function (inputFile) {
    return readUploadedFileAsText(inputFile);
};
```
#### Pages/Index.cshtml


