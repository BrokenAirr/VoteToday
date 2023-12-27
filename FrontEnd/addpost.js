let title = document.getElementById("InputTitle");
let description = document.getElementById("InputDesc");
let inputfile = document.getElementById("InputFile");
let form = document.getElementById("addpostform") ;
let errorp = document.getElementById("ErrorType")

function AddPost(title, desc, file){
    let formdata = new FormData();
    formdata.append("Title",title)
    formdata.append("Description",desc)
    formdata.append("File",file)
    formdata.append("User.Email",localStorage.getItem("email"))
    formdata.append("User.Password",localStorage.getItem("password"))
    fetch('https://localhost:7149/api/My/Posts',{
        method: "POST",
        body: formdata
    })
    .then(response => response.json())
    .then(data => {
        console.log(data)
    if(data.isSuccess){
alert("post successfully created")
    }else{
    errorp.innerHTML = data.errorMessage
    }
    })
    .catch(e => console.error("Error",e));
}

form.addEventListener("submit", function(e){
    e.preventDefault();
    AddPost(title.value, description.value, inputfile.files[0]);
})

inputfile.addEventListener("change", function(e){
    e.preventDefault();
    if(this.files[0]){
        var picture = new FileReader();
picture.readAsDataURL(this.files[0]);
picture.addEventListener('load', function(event) {
    document.getElementById('uploadedimage').setAttribute('src', event.target.result);
  });
    }
})