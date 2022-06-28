<template>
  <div class="m-header">
    <div class="m-header-left"></div>

    <div class="m-header-right">
      <div class="m-header-back">
        <div class="m-icon-back"></div>

        <router-link to="/report">Quay lại trang bắt đầu sử dụng</router-link>

      </div>

      <div class="m-header-account">
        <div class="m-account-role">
          <select class="m-selected" name="" id="">
            <option value="">Nhà hàng Moca</option>
          </select>
        </div>
        <div class="m-account-avatar"></div>
        <div class="m-account-name">nvnam2</div>
        <div class="m-account-icon">
          <i class="fa fa-angle-down" aria-hidden="true"></i>
        </div>
      </div>

      <div class="m-header-icon">
        <!-- <div class="m-icon-call"></div> -->
        <div class="m-bell">
          <div class="m-icon-bell"></div>
        </div>
        <div @click="logout()" class="m-help">
          <div class="m-icon-help"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BaseRequest from "@/apis/baseRequest";
export default {
  components: {

  },
  data() {
    return {


    };
  },


  created() {
  },

  methods: {
    logout(){
      try {
        // Call api
        BaseRequest.get(`Users/Logout`)
          .then(function (res) {
            if(res.data){
              window.location.href = "http://localhost:8080/login";
              localStorage.removeItem('permission');
            }          
          })
          .catch(function (e) {
            console.log(e);  
          });
      } catch (error) {
        console.log(error);
      }
    }

  },
}
</script>

<style scoped>
</style>
