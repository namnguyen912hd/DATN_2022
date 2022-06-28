<template>
    <div class="m-navigation">
      <router-link to="/report" class="m-navigation-header"></router-link>
      <div class="m-navigation-content">
        <a class="m-navigation-item" href="#">
          <div class="nav-item-icon">
            <div class="m-nav-icon m-icon-product"></div>
          </div>
          <router-link to="/report" class="nav-item-text">Báo cáo</router-link>
        </a>
        <a class="m-navigation-item" href="#">
          <div class="nav-item-icon">
            <div class="m-nav-icon m-icon-product"></div>
          </div>
          <router-link to="/calculationUnits" class="nav-item-text">Đơn vị tính</router-link>
        </a>
        <a class="m-navigation-item" href="#">
          <div class="nav-item-icon">
            <div class="m-nav-icon m-icon-product"></div>
          </div>
          <router-link to="/productCategories" class="nav-item-text">Nhóm hàng hóa</router-link>
        </a>
        <a class="m-navigation-item" href="#">
          <div class="nav-item-icon">
            <div class="m-nav-icon m-icon-product"></div>
          </div>
          <router-link to="/products" class="nav-item-text">Hàng hóa</router-link>
        </a>

        <a class="m-navigation-item" href="#">
          <div class="nav-item-icon">
            <div class="m-nav-icon m-icon-product"></div>
          </div>
          <router-link to="/orders" class="nav-item-text">Đơn hàng</router-link>
        </a>

        <a class="m-navigation-item" href="#">
          <div class="nav-item-icon">
            <div class="m-nav-icon m-icon-product"></div>
          </div>
          <router-link to="/users" class="nav-item-text">Nhân viên</router-link>
        </a>
        <a class="m-navigation-item" href="#">
          <div class="nav-item-icon">
            <div class="m-nav-icon m-icon-product"></div>
          </div>
          <router-link to="/roles" class="nav-item-text">Vai trò</router-link>
        </a>
        <!-- <a class="m-navigation-item" href="#">
          <div class="nav-item-icon">
            <div class="m-nav-icon m-icon-product"></div>
          </div>
          <router-link to="/setting" class="nav-item-text">Thiết lập</router-link>
        </a> -->
        <a class="m-navigation-item" href="#">
          <div class="nav-item-icon">
            <div class="m-nav-icon m-icon-product"></div>
          </div>
          <router-link to="/shipper" class="nav-item-text">Shipper</router-link>
        </a>
      </div>
    </div>
</template>

<script>
export default {
    
}
</script>

<style scoped>
    @import url('https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css');
</style>