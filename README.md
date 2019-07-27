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

```c#

@page "/"
<h1>Encrypt and Decrypt</h1>
<div class="form-group">
    @if (strkey.Length>0)
    {
        <div class="alert alert-success" role="alert">
            File got it!!
        </div>
        
    }else
    {

        <div class="alert alert-danger" role="alert">
            File empty
        </div>
    }
    <div class="custom-file">
        <input type="file" ref="inputkeyref" class="custom-file-input" id="customFileLang" lang="en" onchange=@changekey/>
        <label class="custom-file-label" for="customFileLang">search Key</label>
    </div>
    @if (strFile.Length>0)
    {
        <div class="alert alert-success" role="alert">
            File got it!!
        </div>
        
    }else
    {

        <div class="alert alert-danger" role="alert">
            File empty
        </div>
    }

    @if(strkey.Length>0)
    {
        <div class="custom-file mt-1">
            <input type="file" ref="inputfileref" class="custom-file-input" id="customFileLang" lang="en" onchange=@changefile/>
            <label class="custom-file-label" for="customFileLang">search File</label>
        </div>
    }
    
    @if (strFile.Length>0&&strkey.Length>0)
    {
        <div class="row">
            <div class="col-md-6">
                @if(swbol)
                {
                    <button class="btn bg-primary" onclick=@EncryptData>Encrypt</button>
                }
                
            </div>
            <div class="col-md-6">
                @if(!swbol)
                {
                    <button class="btn bg-primary" onclick="@DecryptData">Decrypt</button>
                }
                
            </div>
        </div>    
    }
    <input type="checkbox" bind="@swbol"/>
    @if(swbol)
    {
        int i=-1;
        <div class="form-group">
            <label>Nueva Linea</label>
            <input class="form-control" bind="@newline" />
        </div>
        <button class="btn bg-primary" onclick="@AddLine">Add Line</button>
        <ul class="list-group mt-3">
            @foreach (var item in list)
            {
                i++;
                <li class="list-group-item">@i .-  @item 
                    <span class="badge badge-danger badge-pill" onclick="@(()=>RemoveLine(item))">remove</span>
                </li>   
                
            }        
        </ul>
    }
    else
    {
        <textarea readonly class="form-control" bind="@strencrypt" rows="15"> 
          
        </textarea>
    }
    
</div>

@functions {
    ElementRef inputkeyref;
    ElementRef inputfileref;
    string strkey="";
    string strFile="";
    bool swbol=true;
    List<string> list=new List<string>();
    string strencrypt="";
    string newline="";
    /*protected override async Task OnInitAsync(){
        
    }*/
    void RemoveLine(string item){
        Console.WriteLine("item : " + item);
        string aux=list.First(p=>p.Equals(item));
        list.Remove(aux);
    }
    void AddLine(){
        list.Insert(0,newline);
        newline="";
    }
    async Task changekey(UIChangeEventArgs evt){
        strkey = await ConsoleEncryption.Services.serviceInterop.GetFileData(inputkeyref);
        byte[] decodedBytes = Convert.FromBase64String(strkey);
        strkey = System.Text.Encoding.UTF8.GetString(decodedBytes);
        
    }
    async Task changefile(UIChangeEventArgs evt){
        strFile = await ConsoleEncryption.Services.serviceInterop.GetFileData(inputfileref);
        byte[] decodedBytes = Convert.FromBase64String(strFile);
        strFile = System.Text.Encoding.UTF8.GetString(decodedBytes);
        string[] auxstr= strFile.Split(Environment.NewLine.ToCharArray());
        strencrypt=strFile;
        FillData(auxstr);
    }
    private byte[] base64tosArrayByte(string strbase64){
        
        byte[] decodedBytes=Convert.FromBase64String(strbase64);
        return decodedBytes;
    }
    void FillData(string[] auxstr){
        list=new List<string>();
        foreach (string item in auxstr)
        {
            list.Add(item);
        }
    }
    void EncryptData(){
        //System.Security.Cryptography.Aes
        if(strkey.Length>16)
            strkey=strkey.Substring(0,16);
        byte[] key=System.Text.Encoding.UTF8.GetBytes(strkey);
        string straux="ADOTRMDWOD1QWELK";
        byte[] IV=System.Text.Encoding.UTF8.GetBytes(straux);
        string plaintext=getPlainText();
        byte[] encrypted;
        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            aes.IV=IV;
            aes.Key=key;
            var encryptor=aes.CreateEncryptor(aes.Key,aes.IV);
            using (var ms=new System.IO.MemoryStream())
            {
                using (var csEncrypt=new System.Security.Cryptography.CryptoStream(ms,encryptor,
                System.Security.Cryptography.CryptoStreamMode.Write))
                {
                    using (var swEncrypt=new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plaintext);
                    }
                    encrypted=ms.ToArray();
                    strencrypt=Convert.ToBase64String(encrypted);
                    swbol=!swbol;
                    list=new List<string>();
                }
            }
        }
    }
    void DecryptData(){
        //System.Security.Cryptography.Aes
        if(strkey.Length>16)
            strkey=strkey.Substring(0,16);
        byte[] key=System.Text.Encoding.UTF8.GetBytes(strkey);
        string straux="ADOTRMDWOD1QWELK";
        byte[] IV=System.Text.Encoding.UTF8.GetBytes(straux);
        byte[] encryptedText=base64tosArrayByte(strencrypt);
        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            aes.IV=IV;
            aes.Key=key;
            
            var decryptor=aes.CreateDecryptor(aes.Key,aes.IV);
            using (var ms=new System.IO.MemoryStream(encryptedText))
            {
                using (var csDecrypt=new System.Security.Cryptography.CryptoStream(ms,decryptor,
                System.Security.Cryptography.CryptoStreamMode.Read))
                {
                    using (var swDecrypt=new System.IO.StreamReader(csDecrypt))
                    {
                        strFile=swDecrypt.ReadToEnd();
                        FillData(strFile.Split(Environment.NewLine.ToCharArray()));
                        swbol=!swbol;
                        strencrypt="";
                    }
                }
            }
        }
    }
    private string getPlainText(){
        string plaintext="";
        foreach (string item in list)
        {
            plaintext= plaintext+item+Environment.NewLine;
        }
        return plaintext;
    }
}

```
