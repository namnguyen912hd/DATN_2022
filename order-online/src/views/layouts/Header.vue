<template>
  <v-app>
    <v-app-bar app>
      <v-row class="d-flex justify-space-between align-center">
        <!-- Tab menu -->
        <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
        <!-- End tab menu -->
        <!-- <v-toolbar-title></v-toolbar-title> -->

        <div class="pr-5">
          <!-- Giỏ hàng -->
          <v-tooltip bottom>
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                class="btn__header"
                @click="showCart = !showCart"
                v-bind="attrs"
                v-on="on"
              >
                <v-icon dense>mdi-cart-outline</v-icon>
              </v-btn>
            </template>
            <span>Giỏ hàng</span>
          </v-tooltip>
          <!-- End giỏ hàng -->

          <!-- Giỏ hàng -->
          <v-tooltip bottom>
            <template v-slot:activator="{ on, attrs }">
              <v-btn class="btn__header" v-bind="attrs" v-on="on">
                <v-icon dense>mdi-clipboard-search-outline</v-icon>
              </v-btn>
            </template>
            <span>Tìm kiếm đơn hàng</span>
          </v-tooltip>
          <!-- End giỏ hàng -->
        </div>
      </v-row>
    </v-app-bar>

    <!-- Menu Left-->
    <v-navigation-drawer v-model="drawer" fixed temporary>
      <v-list-item>
        <v-list-item-content>
          <v-list-item-title class="text-h6"> DANH MỤC </v-list-item-title>
        </v-list-item-content>
      </v-list-item>

      <v-divider></v-divider>
      <!-- Món ăn -->
      <v-list dense nav>
        <v-list-item link>
          <v-list-item-icon>
            <v-icon>mdi-food-fork-drink</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>Danh mục món ăn</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <!-- End mon ăn -->

        <!-- Giỏ hàng -->
        <v-list-item link>
          <v-list-item-icon>
            <v-icon>mdi-cart-outline</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>Giỏ hàng của bạn</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <!-- End giỏ hàng -->

        <!-- Theo dõi đơn hàng -->
        <v-list-item link>
          <v-list-item-icon>
            <v-icon>mdi-clipboard-search-outline</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>Theo dõi đơn hàng</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <!-- End theo dõi đơn hàng -->

        <!-- Lịch sử mua hàng -->
        <v-list-item link>
          <v-list-item-icon>
            <v-icon>mdi-clipboard-text-search-outline</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>Lịch sử đơn hàng</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <!-- End Lịch sử mua hàng -->
      </v-list>
    </v-navigation-drawer>
    <!-- End Menu Left-->

    <!-- View của giỏ hàng -->

    <v-navigation-drawer v-model="showCart" right fixed temporary>
      <v-list-item>
        <v-list-item-content>
          <v-list-item-title class="text-h6"> Giỏ Hàng </v-list-item-title>
        </v-list-item-content>
      </v-list-item>

      <v-divider></v-divider>
      <!-- Món ăn -->
      <v-list dense nav>
        <v-list-item v-for="(item, index) in orderInfo" :key="index">
          <v-row>
            <v-col>{{ item.Quantity }}</v-col>
            <v-col>{{ item.Product.ProductName }}</v-col>
            <v-col>
              <v-btn @click="removeItemCart(item.Product.ProductId)">X</v-btn>
            </v-col>
          </v-row>
        </v-list-item>
      </v-list>

      <template v-slot:append>
        <div class="pa-2">
          <v-row class="d-flex">
            <v-col>
              <v-btn block> Đóng </v-btn>
            </v-col>

            <v-col>
              <v-btn block :disabled="orderInfo.length == 0">
                <router-link
                  class="none-style"
                  to="/payment"
                  :disabled="orderInfo.length == 0"
                >
                  Thanh Toán
                </router-link>
              </v-btn>
            </v-col>
          </v-row>
        </div>
      </template>
    </v-navigation-drawer>

    <!-- End view giỏ hàng-->
  </v-app>
</template>

<script>
export default {
  data: () => ({
    // ẩn hiện tab menu
    drawer: null,
    // ẩn hiện giỏ hàng
    showCart: false,
    // danh sách giỏ hàng
    orderInfo: [],
  }),
  mounted() {
    const orderInfo = localStorage.getItem("cart");
    this.orderInfo = orderInfo ? JSON.parse(orderInfo) : [];
  },
  methods: {
    // Xoá sản phẩm trong giỏ hàng
    removeItemCart(productId) {
      const orderInfo = JSON.parse(localStorage.getItem("cart"));
      const index = orderInfo.findIndex((x) => {
        return x.Product.ProductId === productId;
      });
      orderInfo.splice(index, 1);
      localStorage.setItem("cart", JSON.stringify(orderInfo));
      this.orderInfo = JSON.parse(localStorage.getItem("cart"));
    },

    // Update giỏ hàng
    updateQuantityItemCart(ProductId, quantity) {
      let isExist = this.orderInfo.some((x) => {
        return x.Product.ProductId == ProductId;
      });
      if (isExist) {
        let item = this.orderInfo.find((x) => {
          return x.Product.ProductId == ProductId;
        });
        item.Quantity = quantity;
        localStorage.setItem("cart", JSON.stringify(this.orderInfo));
        
      }
    },
  },
};
</script>

<style lang="scss" scoped>
::v-deep .btn__header {
  border: none;
  background-color: transparent;
  box-shadow: none;
}

.none-style {
  text-decoration: none;
  color: #000000;
}
</style>