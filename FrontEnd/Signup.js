let email = document.getElementById("mail");
let password = document.getElementById("psw");
let errorp = document.getElementById("ErrorType")
let form = document.getElementById("signupform")

function SignUp(mail, psw){
let data = {
    email: mail,
    password: psw
};
fetch('https://localhost:7149/api/My/Registration',{
    method: "POST",
    body: JSON.stringify(data),
    headers:{
        "Content-Type": "application/json"
    }
})
.then(response => response.json())
.then(data => {
    console.log(data);
    if(data.isSuccess){
    localStorage.setItem("email", data.result.email);
    localStorage.setItem("password", data.result.password);
    localStorage.setItem("credexpiration", Date.now() + 60000);
    window.location.href = "/index.html"        
    }else{
errorp.innerHTML = data.errorMessage;
    }

})
.catch(e => console.error("Error",e));
}

form.addEventListener("submit", function(e){
    e.preventDefault();
    SignUp(email.value, password.value);
})