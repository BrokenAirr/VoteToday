let Posts = document.getElementById("Posts")

function Vote(postid, whatvote, postvotes){
    let data = {
        PostId: postid,
        Email: localStorage.getItem("email"),
        Password: localStorage.getItem("password"),
        whatvote: whatvote
    };
    fetch('https://localhost:7149/api/My/Posts/Vote',{
        method: "POST",
        body: JSON.stringify(data),
        headers:{
            "Content-Type": "application/json"
        }
    })
    .then(response => response.json())
    .then(data => {
    if(data.isSuccess){
        postvotes.innerHTML = `<span class="agrees">agrees: ${data.result.trues}</span>  <span class="disagrees">disagrees: ${data.result.falses}</span>`;
    }
    })
    .catch(e => console.error("Error",e));
}
function GetPosts(){
    fetch('https://localhost:7149/api/My/Posts',{
        method: "GET",
        headers:{
            "Content-Type": "application/json"
        }
    })
    .then(response => response.json())
    .then(data => {
        console.log(data)
        if(data.isSuccess){
            Posts.innerHTML = "";
            data.result.forEach(element => {
                let postDiv = document.createElement("div");
                postDiv.className = "Carde";
                let postImg = document.createElement("img");
                postImg.src = "data:image/png;base64," + element.image;
                postDiv.appendChild(postImg);
                let postTitle = document.createElement("h2");
                postTitle.textContent = element.title;
                postDiv.appendChild(postTitle);
                let postDesc = document.createElement("p");
                postDesc.textContent = element.description;
                postDiv.appendChild(postDesc);
                let postVotes = document.createElement("h3");
                postVotes.innerHTML = `<span class="agrees">agrees: ${element.trues}</span>  <span class="disagrees">disagrees: ${element.falses}</span>`;
                postDiv.appendChild(postVotes);
                let agreeBtn = document.createElement("button");
                agreeBtn.className = "btn btn-outline-success agree";
                agreeBtn.type = "submit";
                agreeBtn.innerHTML = `<i class="bi bi-check-lg"></i>`;


                agreeBtn.addEventListener('click', function(e){
                    e.preventDefault();
                    Vote(element.id, true, postVotes)
                });


                postDiv.appendChild(agreeBtn);
                let disagreeBtn = document.createElement("button");
                disagreeBtn.className = "btn btn-outline-danger disagree";
                disagreeBtn.type = "submit";
                disagreeBtn.innerHTML = `<i class="bi bi-x-lg"></i>`;


                disagreeBtn.addEventListener('click', function(e){
                    e.preventDefault();
                    Vote(element.id,false,postVotes)
                });


                postDiv.appendChild(disagreeBtn);
                Posts.appendChild(postDiv);
            });
        }
    })
    .catch(e => console.error("Error",e));
    }
GetPosts();