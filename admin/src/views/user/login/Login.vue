<template>
  <div style="height: 100vh">
    <div class="overlay">
    </div>
    <div class="login-form">
      <h1 style="text-align:center">Nhà Hàng Moca</h1>
      <div class="imgcontainer">
        <img src="../../../assets/content/img/img_avatar2.png" alt="Avatar" class="avatar">
      </div>

      <div class="container">
        <label for="userName"><b>Tên đăng nhập</b></label>
        <input v-model="username" type="text" placeholder="Tên đăng nhập" name="userName" required>

        <label for="password"><b>Mật khẩu</b></label>
        <input v-model="pwd" type="password" placeholder="Enter Password" name="password" required>

        <button @click="login()" >Đăng nhập</button>

      </div>
    </div>


  </div>
</template>

<script>
import BaseRequest from "@/apis/baseRequest";
export default {
  components: {},
  data() {
    return {
 
        username: null,
        pwd: null,

      }
  },
  methods: {
    login() {
      try {
        // Call api
        BaseRequest.post(`Users/Login`,{UserName:this.username, password:this.pwd})
          .then(function (res) {
            if(res.data === 400){
              alert('Sai tên đăng nhập hoặc mật khẩu')
            }
            else{
              let lstPermission = res.data;
              
              localStorage.setItem('permission', res.data);
              if(lstPermission.includes('shipper')){
                window.location.href = "http://localhost:8080/shipper";
              }
              else{
                window.location.href = "http://localhost:8080/report";
              }
              
            }          
          })
          .catch(function (e) {
            console.log(e);  
          });
      } catch (error) {
        console.log(error);
      }
    },
  }

}
</script>

<style scoped>
.overlay {
  position: absolute;
  top: 0px;
  right: 0px;
  left: 0;
  bottom: 0;
  background-color: #beb2a1;
  opacity: 1;
  z-index: 1400;
}

.login-form {
  border-radius: 4px;
  background-color: #f1f1f1;
  position: absolute;
  top: 16%;
  left: 35%;
  border: 3px solid #f1f1f1;
  display: flex;
  width: 32%;
  flex-direction: column;
  z-index: 15000;
}

input[type=text],
input[type=password] {
  width: 100%;
  padding: 12px 20px;
  margin: 8px 0;
  display: inline-block;
  border: 1px solid #ccc;
  box-sizing: border-box;
  opacity: 1;
}

button {
  background-color: #04AA6D;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
}

button:hover {
  opacity: 0.8;
}

.cancelbtn {
  width: auto;
  padding: 10px 18px;
  background-color: #f44336;
}

.imgcontainer {
  text-align: center;
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 24px 0 12px 0;
}

img.avatar {
  width: 25%;
  border-radius: 50%;
}

.container {
  padding: 16px;
}

span.psw {
  float: right;
  padding-top: 16px;
}

/* Change styles for span and cancel button on extra small screens */
@media screen and (max-width: 300px) {
  span.psw {
    display: block;
    float: none;
  }

  .cancelbtn {
    width: 100%;
  }
}
</style>