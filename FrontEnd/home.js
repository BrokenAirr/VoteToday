if(localStorage.getItem("credexpiration") < Date.now()){
    localStorage.clear();
    console.log("expired")
}else{
    console.log(localStorage.getItem("email"), localStorage.getItem("password"))
    AutoLogin()
}
function AutoLogin() {
    let data = {
        email: localStorage.getItem("email"),
        password: localStorage.getItem("password")
    };
    fetch('https://jsonplaceholder.typicode.com/todos',{
        method: "POST",
        body: JSON.stringify(data),
        headers:{
            "Content-Type": "application/json"
        }
    })
    .then(response => response.json())
    .then(data => {
        let whenlogged = document.querySelectorAll(".whenlogged")
        let whennotlogged = document.querySelectorAll(".whennotlogged")
        let name = document.getElementById("usersname")
        name.innerHTML = data.email
for (let index = 0; index < whenlogged.length; index++) {
    const element = whenlogged[index];
    element.classList.remove("invis")
}
for (let index = 0; index < whennotlogged.length; index++) {
    const element = whennotlogged[index];
    element.classList.add("invis")
}
    })
    .catch(e => console.error("Error",e));
}
let logoutbtn = document.getElementById("LogOutBtn");
logoutbtn.addEventListener("click", function(e){
    localStorage.clear();
    window.location.href = "/index.html"
})