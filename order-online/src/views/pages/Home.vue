<template>
  <v-app id="inspire">
    <v-app-bar app>
      <v-row class="d-flex justify-space-between align-center">
        <!-- Tab menu -->
        <div>
          <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
          <!-- End tab menu -->
          <v-btn class="btn__header">
            <div class="d-flex align-center">
              <v-icon>mdi-home</v-icon>Trang chủ
            </div>
          </v-btn>
        </div>
        <div class="pr-5">
          <!-- Nút giỏ hàng -->
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
          <!-- End Nút giỏ hàng-->

          <!-- Tìm kiếm đơn hàng  -->
          <v-tooltip bottom>
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                class="btn__header"
                v-bind="attrs"
                v-on="on"
                @click="showHistory = !showHistory"
              >
                <v-icon dense>mdi-clipboard-search-outline</v-icon>
              </v-btn>
            </template>
            <span>Tìm kiếm đơn hàng</span>
          </v-tooltip>
          <!-- End Tìm kiếm đơn hàng -->
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

    <v-main class="grey lighten-2">
      <v-container>
        <!-- Danh sách sản phẩm -->
        <!-- Tìm kiếm -->
        <v-row class="d-flex align-end">
          <!-- Lấy theo danh mục -->
          <v-col cols="6">
            <carousel
              v-if="lstCategory.length > 0"
              :items="4"
              :nav="false"
              :dots="false"
              :margin="10"
              :autoWidth="true"
              :autoHeight="true"
            >
              <div
                class="tab-menu-category"
                v-for="cate in lstCategory"
                :key="cate.ProductCategoryId"
              >
                <v-btn
                  color="error"
                  outlined
                  :disabled="cate.lstProduct.length == 0"
                  @click="
                    getProductsByCategoryID(
                      cate.ProductCategoryId,
                      cate.ProductCategoryName
                    )
                  "
                >
                  {{ cate.ProductCategoryName }}
                </v-btn>
              </div>
            </carousel>
            <v-btn
              small
              color="error"
              v-if="cateID != null"
              @click="clearFilter()"
            >
              {{ cateName }} <v-icon small> mdi-close </v-icon>
            </v-btn>
          </v-col>

          <v-col cols="2"></v-col>
          <!-- Tìm kiếm -->
          <v-col cols="4">
            <v-text-field
              label="Tìm kiếm món ăn"
              hide-details="auto"
              v-model="textSearch"
              @blur="SearchProduct()"
            >
              <v-icon slot="append" dense> mdi-magnify </v-icon>
            </v-text-field>
          </v-col>
        </v-row>
        <!-- End tìm kiếm -->

        <v-row v-if="lstProductFilter.length == 0 && !isSearch">
          <template v-for="(cate, index) in lstCategory">
            <template
              v-if="cate.lstProduct != [] && cate.lstProduct.length > 0"
            >
              <v-col :key="`cateName${index}`" class="mt-2" cols="12">
                <h2>
                  <strong>{{ cate.ProductCategoryName }}</strong>
                </h2>
              </v-col>
              <carousel
                :items="4"
                :key="`item${index}`"
                :autoWidth="true"
                :autoHeight="true"
                :loop="cate.lstProduct.length > 5"
                :margin="3"
                :nav="false"
                :dots="false"
                v-if="cate.lstProduct.length > 0"
              >
                <v-col v-for="item in cate.lstProduct" :key="item.ProductId">
                  <div class="item">
                    <div class="item-image">
                      <img
                        v-bind:src="
                          'https://localhost:44348/api/v1/ProductImages/Image?id=' +
                          item.ProductImageId
                        "
                        v-if="item.ProductImageId && item.ProductImageId"
                      />
                      <img
                        src="@/assets/images/noimage.jpg"
                        v-if="!item.ProductImageId"
                      />
                    </div>
                    <div class="item-name">
                      <b
                        >{{ item.ProductName }}({{
                          item.CalculationUnitName
                        }})</b
                      >
                    </div>
                    <div class="item-price">
                      <b>{{ formatPrice(item.SellingPrice) }} VND</b>
                    </div>
                    <div class="item-btn">
                      <!-- Chi tiết món ăn -->
                      <v-dialog
                        transition="dialog-top-transition"
                        max-width="600"
                      >
                        <template v-slot:activator="{ on, attrs }">
                          <v-btn
                            color="error"
                            v-bind="attrs"
                            v-on="on"
                            @click="showItemDetail(item.ProductId)"
                          >
                            Thêm vào giỏ hàng
                          </v-btn>
                        </template>
                        <template v-slot:default="dialog">
                          <v-card>
                            <v-toolbar color="primary" dark>
                              Thêm giỏ hàng
                            </v-toolbar>
                            <v-card-text>
                              <v-row class="item-detail">
                                <v-col cols="5">
                                  <div class="item-detail-img">
                                    <img
                                      :src="
                                        'https://localhost:44348/api/v1/ProductImages/Image?id=' +
                                        item.ProductImageId
                                      "
                                      v-if="
                                        item.ProductImageId &&
                                        item.ProductImageId
                                      "
                                    />
                                    <img
                                      src="@/assets/images/noimage.jpg"
                                      v-if="!item.ProductImageId"
                                    />
                                  </div>
                                </v-col>
                                <v-col cols="7">
                                  <div class="item-detail-name">
                                    {{ product.ProductName }}
                                  </div>
                                  <div class="item-detail-price">
                                    {{ formatPrice(product.SellingPrice) }} VND
                                  </div>
                                  <div class="item-detail-quantity">
                                    <label for="">Số lượng</label>
                                    <v-text-field
                                      v-model="quantity"
                                      hide-details
                                      single-line
                                      type="number"
                                    />
                                  </div>
                                </v-col>
                              </v-row>
                              <v-row>
                                <v-col>
                                  <v-textarea
                                    clearable
                                    outlined
                                    clear-icon="mdi-close-circle"
                                    rows="3"
                                    label="Ghi chú "
                                  ></v-textarea>
                                </v-col>
                              </v-row>
                            </v-card-text>
                            <v-card-actions class="justify-end">
                              <!-- nút đóng form giỏ hàng -->
                              <v-btn
                                color="error"
                                @click="dialog.value = false"
                              >
                                Huỷ
                              </v-btn>
                              <!-- Chuyển sang trag thanh toán -->
                              <v-btn
                                color="primary"
                                @click="
                                  dialog.value = false;
                                  updateCart(product, quantity);
                                "
                              >
                                Đồng ý
                              </v-btn>
                            </v-card-actions>
                          </v-card>
                        </template>
                      </v-dialog>
                    </div>
                  </div>
                </v-col>
              </carousel>
            </template>
          </template>
        </v-row>

        <v-row v-if="lstProductFilter.length > 0">
          <v-col
            v-for="item in lstProductFilter"
            :key="item.ProductId"
            cols="3"
          >
            <div class="item">
              <div class="item-image">
                <img
                  v-bind:src="
                    'https://localhost:44348/api/v1/ProductImages/Image?id=' +
                    item.ProductImageId
                  "
                  v-if="item.ProductImageId && item.ProductImageId"
                />
                <img
                  src="@/assets/images/noimage.jpg"
                  v-if="!item.ProductImageId"
                />
              </div>
              <div class="item-name">
                <b>{{ item.ProductName }}({{ item.CalculationUnitName }})</b>
              </div>
              <div class="item-price">
                <b>{{ formatPrice(item.SellingPrice) }} VND</b>
              </div>
              <div class="item-btn">
                <!-- Chi tiết món ăn -->
                <v-dialog transition="dialog-top-transition" max-width="600">
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn
                      color="error"
                      v-bind="attrs"
                      v-on="on"
                      @click="showItemDetail(item.ProductId)"
                    >
                      Thêm vào giỏ hàng
                    </v-btn>
                  </template>
                  <template v-slot:default="dialog">
                    <v-card>
                      <v-toolbar color="primary" dark>
                        Thêm giỏ hàng
                      </v-toolbar>
                      <v-card-text>
                        <v-row class="item-detail">
                          <v-col cols="5">
                            <div class="item-detail-img">
                              <img
                                :src="
                                  'https://localhost:44348/api/v1/ProductImages/Image?id=' +
                                  item.ProductImageId
                                "
                                v-if="
                                  item.ProductImageId && item.ProductImageId
                                "
                              />
                              <img
                                src="@/assets/images/noimage.jpg"
                                v-if="!item.ProductImageId"
                              />
                            </div>
                          </v-col>
                          <v-col cols="7">
                            <div class="item-detail-name">
                              {{ product.ProductName }}
                            </div>
                            <div class="item-detail-price">
                              {{ formatPrice(product.SellingPrice) }} VND
                            </div>
                            <div class="item-detail-quantity">
                              <label for="">Số lượng</label>
                              <v-text-field
                                v-model="quantity"
                                hide-details
                                single-line
                                type="number"
                              />
                            </div>
                          </v-col>
                        </v-row>
                        <v-row>
                          <v-col>
                            <v-textarea
                              clearable
                              outlined
                              clear-icon="mdi-close-circle"
                              rows="3"
                              label="Ghi chú "
                            ></v-textarea>
                          </v-col>
                        </v-row>
                      </v-card-text>
                      <v-card-actions class="justify-end">
                        <!-- nút đóng form giỏ hàng -->
                        <v-btn color="error" @click="dialog.value = false">
                          Huỷ
                        </v-btn>
                        <!-- Chuyển sang trag thanh toán -->
                        <v-btn
                          color="primary"
                          @click="
                            dialog.value = false;
                            updateCart(product, quantity);
                          "
                        >
                          Đồng ý
                        </v-btn>
                      </v-card-actions>
                    </v-card>
                  </template>
                </v-dialog>
              </div>
            </div>
          </v-col>
        </v-row>

        <v-row v-if="lstProductFilter.length == 0 && isSearch">
          <h2>Không tìm thấy món bạn cần tìm</h2>
        </v-row>
        <!-- End danh sách sản phẩm -->
      </v-container>

      <!-- View của giỏ hàng -->
      <v-navigation-drawer
        v-model="showCart"
        :width="400"
        right
        fixed
        temporary
      >
        <template> </template>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title class="text-h6"> Giỏ Hàng </v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-divider></v-divider>
        <!-- Món ăn -->
        <v-list dense nav>
          <v-simple-table>
            <template v-slot:default :fixed-header="true" height="496px">
              <thead>
                <tr>
                  <th class="text-left" :width="200">Tên món</th>
                  <th class="text-left">Số lượng</th>
                  <th class="text-left">Thành tiền</th>
                  <th class="text-left">Xoá</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(item, index) in orderInfo" :key="index">
                  <td class="item-cart-name">{{ item.Product.ProductName }}</td>
                  <td>
                    <div>x{{ item.Quantity }}</div>
                  </td>
                  <td>
                    {{
                      formatPrice(item.Quantity * item.Product.SellingPrice)
                    }}đ
                  </td>
                  <td>
                    <v-btn
                      fab
                      dark
                      small
                      color="error"
                      @click="removeItemCart(item.Product.ProductId)"
                    >
                      <v-icon> mdi-delete </v-icon>
                    </v-btn>
                  </td>
                </tr>
              </tbody>
            </template>
          </v-simple-table>
        </v-list>

        <template v-slot:append>
          <div style="padding: 0 16px; text-align: right">
            <h3>
              <b> Tổng tiền: {{ formatPrice(totalAmount) }}đ</b>
            </h3>
          </div>
          <div class="pa-2">
            <v-row class="d-flex">
              <v-col>
                <v-btn block @click="showCart = false"> Đóng </v-btn>
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

      <!-- View của theo dõi đơn hàng -->
      <v-navigation-drawer
        v-model="showHistory"
        :width="400"
        right
        fixed
        temporary
      >
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title class="text-h6"
              >Tìm kiếm đơn hàng
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-divider></v-divider>
        <!-- Món ăn -->
        <v-list dense nav>
          <v-text-field
            label="Nhập số điện thoại"
            hide-details="auto"
            outlined
            clearable
            v-model="searchHistory"
            @blur="searchOrdersByTelephoneCustomer()"
          >
          </v-text-field>

          <v-row class="list-order" v-if="lstOrderHistory.length > 0">
            <v-col
              cols="12"
              v-for="(order, index) in lstOrderHistory"
              :key="index"
            >
              <v-card class="mx-auto order">
                <div class="order-code">
                  <b>Số hoá đơn: </b>{{ order.OrderCode }}
                </div>
                <div class="order-customer-name">
                  <b>Tên người nhận: </b>{{ order.CustomerName }}
                </div>
                <div class="order-total-amount">
                  <b>Ngày đặt: </b>{{ formatDateTime(order.OrderDate) }}
                </div>
                <div class="d-flex justify-space-between">
                  <div class="order-status">
                    <b>Trạng thái: </b>
                    <span v-if="order.OrderStatus == 0">Chưa xác nhận</span>
                    <span v-if="order.OrderStatus == 1" style="color: #0000ff"
                      >Đã xác nhận</span
                    >
                    <span v-if="order.OrderStatus == 2" style="color: #00ffff"
                      >Đang giao</span
                    >
                    <span v-if="order.OrderStatus == 3" style="color: #00ff00"
                      >Hoàn thành</span
                    >
                    <span v-if="order.OrderStatus == 4" style="color: #ff0000"
                      >Đã huỷ</span
                    >
                  </div>
                  <div>
                    <v-dialog
                      transition="dialog-bottom-transition"
                      max-width="600"
                    >
                      <template v-slot:activator="{ on, attrs }">
                        <v-btn
                          text
                          v-bind="attrs"
                          v-on="on"
                          small
                          style="color: red"
                          >Chi tiết >></v-btn
                        >
                      </template>
                      <template v-slot:default="dialog">
                        <v-card>
                          <v-toolbar color="error" dark
                            >Chi tiết đơn hàng {{ order.OrderCode }}</v-toolbar
                          >
                          <v-card-text class="list-order">
                            <div class="order">
                              <div>
                                <b>Tên người nhận:</b> {{ order.CustomerName }}
                              </div>
                              <div><b>Email:</b> {{ order.Email }}</div>
                              <div>
                                <b>Địa chỉ:</b> {{ order.ReceviedAddress }}
                              </div>
                              <div><b>Hình thức thanh toán:</b> Tiền mặt</div>
                              <div>
                                <b>Phí giao hàng:</b>
                                {{ formatPrice(order.ShippingCost) }}đ
                              </div>
                              <div>
                                <b>Tổng thành tiền:</b>
                                {{ formatPrice(order.TotalAmount) }}đ
                              </div>
                              <div>
                                <b>Có lấy hoá đơn:</b>
                                {{ order.IsPrint == 1 ? "Có" : "Không" }}
                              </div>
                            </div>
                            <!-- Danh sachs món đặt -->
                            <h3 style="margin-left: 15px">Danh sách món đặt</h3>
                            <v-simple-table>
                              <template
                                v-slot:default
                                :fixed-header="true"
                                height="306px"
                              >
                                <thead>
                                  <tr>
                                    <th class="text-left" :width="200">
                                      Tên món
                                    </th>
                                    <th class="text-left">Số lượng</th>
                                    <th class="text-left">Thành tiền</th>
                                  </tr>
                                </thead>
                                <tbody>
                                  <tr
                                    v-for="(item, index) in order.OrderDetail"
                                    :key="index"
                                  >
                                    <td class="item-cart-name">
                                      {{ item.ProductName }}
                                    </td>
                                    <td>
                                      <div>x{{ item.Quantity }}</div>
                                    </td>
                                    <td>
                                      {{
                                        formatPrice(
                                          item.Quantity * item.UnitPrice
                                        )
                                      }}đ
                                    </td>
                                  </tr>
                                </tbody>
                              </template>
                            </v-simple-table>

                            <!-- Trạng thái đơn hàng -->
                            <div class="font-weight-bold ml-8 mb-2">
                              Trạng thái đơn hàng
                            </div>

                            <v-timeline align-top dense>
                              <!-- Khách đặt món -->
                              <v-timeline-item color="white" small>
                                <div>
                                  <div class="font-weight-normal">
                                    <strong
                                      >Khách hàng
                                      {{ order.CustomerName }}</strong
                                    >
                                    @{{ formatDateTime(order.OrderDate) }}
                                  </div>
                                  <div>Khách hàng đặt món</div>
                                </div>
                              </v-timeline-item>
                              <!-- NV chấp nhận đơn hàng -->
                              <v-timeline-item
                                color="deep-purple"
                                small
                                v-if="order.UserComfirmName != null"
                              >
                                <div>
                                  <div class="font-weight-normal">
                                    <strong
                                      >Nhân viên
                                      {{ order.UserComfirmName }}</strong
                                    >
                                    @{{
                                      formatDateTime(order.ConfirmationTime)
                                    }}
                                  </div>
                                  <div>Nhân viên đã xác nhận đơn hàng</div>
                                </div>
                              </v-timeline-item>
                              <!-- Ship giao hàng -->
                              <v-timeline-item
                                color="blue"
                                small
                                v-if="
                                  order.ShipperName != null &&
                                  order.DeliveryTime != null
                                "
                              >
                                <div>
                                  <div class="font-weight-normal">
                                    <strong
                                      >Shipper {{ order.ShipperName }}</strong
                                    >
                                    @{{ formatDateTime(order.DeliveryTime) }}
                                  </div>
                                  <div>
                                    Shipper {{ order.ShipperName }} đang giao
                                    hàng
                                  </div>
                                </div>
                              </v-timeline-item>
                              <!-- Khách nhận hàng thành công -->
                              <v-timeline-item
                                color="green"
                                small
                                v-if="
                                  order.ShipperName != null &&
                                  order.OrderStatus == 3
                                "
                              >
                                <div>
                                  <div class="font-weight-normal">
                                    <strong
                                      >Shipper {{ order.ShipperName }}</strong
                                    >
                                    @{{ formatDateTime(order.ModifiedDate) }}
                                  </div>
                                  <div>Khách hàng đã nhận hàng</div>
                                </div>
                              </v-timeline-item>
                              <!-- Huỷ hàng -->
                              <v-timeline-item
                                v-if="order.OrderStatus == 4"
                                color="red"
                                small
                              >
                                <div>
                                  <div class="font-weight-normal">
                                    <strong v-if="order.ShipperName == null">
                                      {{ order.UserComfirmName }}
                                    </strong>
                                    <strong v-if="order.ShipperName != null">
                                      {{ order.ShipperName }}
                                    </strong>
                                    @{{ formatDateTime(order.ModifiedDate) }}
                                  </div>
                                  <div v-if="order.ShipperName == null">
                                    Nhân viên {{ order.UserComfirmName }} huỷ
                                    đơn hàng
                                  </div>
                                  <div v-if="order.ShipperName != null">
                                    {{ order.UserComfirmName }} huỷ đơn hàng
                                  </div>
                                </div>
                              </v-timeline-item>
                            </v-timeline>
                          </v-card-text>
                          <v-card-actions class="justify-end">
                            <v-btn text @click="dialog.value = false"
                              >Đóng</v-btn
                            >
                          </v-card-actions>
                        </v-card>
                      </template>
                    </v-dialog>
                  </div>
                </div>
              </v-card>
            </v-col>
          </v-row>

          <div
            class="list-order-not"
            v-if="lstOrderHistory.length == 0 && isSearchHistory"
          >
            Không tìm thấy đơn hàng nào
          </div>
        </v-list>
      </v-navigation-drawer>
      <!-- End view theo dõi đơn hàng-->
    </v-main>
  </v-app>
</template>

<script>
import api from "@/services/base/api";

import carousel from "vue-owl-carousel";
//import func from 'vue-editor-bridge';
export default {
  components: { carousel },

  data: () => ({
    apiCategory: "ProductCategories",
    apiProduct: "Products",
    apiOrder: "Orders",
    // ẩn hiện tab menu
    drawer: null,
    // Danh sach danh mục
    lstCategory: [],
    // HIển thị giỏ hàng
    showCart: false,
    // Danh sách món ăn
    lstProduct: [],
    // THông tin giỏ hàng
    orderInfo: [],
    // Chi tiết sản phẩm
    product: {},
    // Số lượng giỏ hàng
    quantity: 1,
    // Tổng tiền giỏ hàng
    totalAmount: 0,
    // Danh sách product tìm kiếm
    lstProductFilter: [],

    cateName: null,

    textSearch: "",
    cateID: null,
    isSearch: false,
    showHistory: false,
    lstOrderHistory: [],
    searchHistory: "",
    isSearchHistory: false,
  }),

  created() {
    this.getAllProductByCategory();
  },

  mounted() {
    const orderInfo = localStorage.getItem("cart");
    this.orderInfo = orderInfo ? JSON.parse(orderInfo) : [];
  },

  methods: {
    searchOrdersByTelephoneCustomer() {
      this.lstOrderHistory = [];
      this.isSearchHistory = true;
      if (this.searchHistory == null || this.searchHistory == "") {
        this.isSearchHistory = false;
      } else {
        api
          .get(
            `${
              this.apiOrder
            }/GetOrdersByTelephoneCustomer/${this.searchHistory.toString()}`
          )
          .then((response) => {
            this.lstOrderHistory = response.data;
          })
          .catch((e) => {
            console.log(e);
          });
      }
    },

    // tìm kiếm món ăn
    SearchProduct() {
      if (!this.cateID) {
        this.cateID = null;
      }
      if (!this.textSearch || this.textSearch == "") {
        if (this.cateID) {
          this.getProductsByCategoryID(this.cateID, this.cateName);
        } else {
          this.lstProductFilter = [];
        }
      } else {
        this.getProductsByCategoryID(this.cateID, this.cateName);
      }
    },
    // Lấy món theo id loại
    getProductsByCategoryID(cateID, cateName) {
      this.cateName = cateName;
      this.cateID = cateID;
      api
        .get(
          `${this.apiProduct}/GetProcductsByCategoryID/?id=${cateID}&name=${this.textSearch}`
        )
        .then((response) => {
          this.lstProductFilter = response.data;
          if (
            (this.cateID != null || this.textSearch != null) &&
            this.lstProductFilter.length == 0
          ) {
            this.isSearch = true;
          } else {
            this.isSearch = false;
          }
        })
        .catch((e) => {
          console.log(e);
        });
    },
    // Lay all
    getAllProductByCategory() {
      const me = this;
      api
        .get(`${me.apiCategory}/getAllProductByCategory`)
        .then((response) => {
          me.lstCategory = response.data;
        })
        .catch((e) => {
          console.log(e);
        });
    },
    // Xoá chọn danh mục
    clearFilter() {
      this.cateName = null;
      this.cateID = null;
      this.lstProductFilter = [];
    },
    // Thêm sản phẩm vào giỏ hàng
    updateCart(product, quantity) {
      let isExist = this.orderInfo.some((x) => {
        return x.Product.ProductId == product.ProductId;
      });
      quantity = parseInt(quantity);
      if (isExist) {
        //Nếu có update số lượng
        let item = this.orderInfo.find((x) => {
          return x.Product.ProductId == product.ProductId;
        });
        item.Quantity += quantity;
        localStorage.setItem("cart", JSON.stringify(this.orderInfo));
      } //Nếu không có thì thêm mới
      else {
        let itemCart = {
          Quantity: quantity,
          Product: product,
        };
        this.orderInfo.push(itemCart);
        localStorage.setItem("cart", JSON.stringify(this.orderInfo));
      }
      //toast messe+ge
      this.$toast.success("Thêm giỏ hàng thành công!", {
        position: "top-right",
        timeout: 5000,
        closeOnClick: true,
        pauseOnFocusLoss: true,
        pauseOnHover: true,
        draggable: true,
        draggablePercent: 0.6,
        showCloseButtonOnHover: false,
        hideProgressBar: false,
        closeButton: false,
        icon: true,
        rtl: false,
      });
    },

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

    showItemDetail(id) {
      const me = this;
      this.diglog = true;
      this.quantity = 1;
      api
        .getId(`Products`, id)
        .then((response) => {
          me.product = response.data;
        })
        .catch((e) => {
          console.log(e);
        });
    },

    formatPrice(value) {
      let val = (value / 1).toFixed(0).replace(".", ",");
      return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },
    formatDateTime(datetime) {
      var today = new Date(datetime);
      var time =
        (today.getDate() < 10 ? "0" : "") +
        today.getDate() +
        "/" +
        (today.getMonth() + 1 < 10 ? "0" : "") +
        (today.getMonth() + 1) +
        "/" +
        today.getFullYear().toString() +
        " " +
        (today.getHours() < 10 ? "0" : "") +
        today.getHours() +
        ":" +
        (today.getMinutes() < 10 ? "0" : "") +
        today.getMinutes();
      return time;
    },
    // TÍnh tổng tiền
    calcTotalAmount() {
      if (this.orderInfo) {
        this.totalAmount = 0;
        this.orderInfo.forEach((item) => {
          this.totalAmount += item.Product.SellingPrice * item.Quantity;
        });
      }
    },
  },

  watch: {
    orderInfo() {
      // Hàm tính tiền
      this.calcTotalAmount();
    },
    showCart() {
      const orderInfo = localStorage.getItem("cart");
      this.orderInfo = orderInfo ? JSON.parse(orderInfo) : [];
    },
    searchHistory() {},
  },
};
</script>


<style lang="scss" scoped>
::v-deep .btn__header {
  border: none;
  background-color: transparent;
  box-shadow: none;
}
.v-application .grey.lighten-2 {
  border-color: #fff !important;
  background-color: #fff !important;
}

.none-style {
  text-decoration: none;
  color: #000000;
}

.item {
  width: 287px;
  margin-bottom: 20px;
  background-color: white;
  float: left;
  border-radius: 8px;
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
  .item-image {
    height: 191px;
    overflow: hidden;
    box-sizing: border-box;
    img {
      width: 100%;
      height: 100%;
      object-fit: contain;
      max-height: 250px;
      border-radius: 8px;
      transition-duration: 0.6s;
      border-bottom-right-radius: 0px;
      border-bottom-left-radius: 0px;
    }
  }
  .item-name {
    text-align: center;
    font-size: 24px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    overflow: hidden;
    display: -webkit-box;
    height: 58px;
    padding: 0 10px;
  }
  .item-price {
    color: red;
    text-align: center;
    font-size: 20px;
  }
  .item-btn {
    padding: 10px 0;
    text-align: center;
  }
}

.item:hover .item-image img {
  background-size: cover;
  transform: scale(1.1);
  transition-duration: 0.6s;
}

.tab-menu-category {
  margin-bottom: 5px;
}

.item-detail {
  margin-top: 10px;
  .item-detail-img {
    width: 100%;
    height: auto;
    img {
      width: 100%;
      height: 100%;
      object-fit: contain;
    }
  }
  .item-detail-name {
    padding: 10px 0;
    font-size: 30px;
    font-weight: bold;
  }
  .item-detail-price {
    padding: 10px 0;
    font-size: 20px;
    color: red;
    font-weight: bold;
  }
  .item-cart-name {
    text-align: center;
    font-size: 24px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    overflow: hidden;
    display: -webkit-box;
  }
}

.list-order {
  .order {
    margin-top: 10px;
    padding: 10px 20px;
    div {
      margin: 2px 0;
    }
  }
}
</style>