<template>
  <v-app>
    <v-main>
      <v-container>
        <form>
          <v-row>
            <v-col cols="6">
              <h1>Thông tin khách hàng</h1>
              <!-- Họ và tên -->
              <v-text-field
                v-model="name"
                label="Họ và tên"
                required
                :error-messages="nameErrors"
                @input="$v.name.$touch()"
                @blur="$v.name.$touch()"
              ></v-text-field>

              <!-- Số điện thoại -->
              <v-text-field
                v-model="numberPhone"
                label="Số điện thoại"
                type="number"
                required
                :error-messages="numberPhoneErrors"
                @input="$v.numberPhone.$touch()"
                @blur="$v.numberPhone.$touch()"
              ></v-text-field>

              <!-- Email -->
              <v-text-field
                v-model="email"
                label="E-mail"
                required
                :error-messages="emailErrors"
                @input="$v.email.$touch()"
                @blur="$v.email.$touch()"
              ></v-text-field>

              <!-- Địa chỉ-->
              <v-text-field
                v-model="address"
                label="Địa chỉ giao hàng"
                required
                :error-messages="addressErrors"
                @input="$v.address.$touch()"
                @blur="$v.address.$touch()"
              ></v-text-field>

              <!-- Ghi chú -->
              <v-textarea
                clearable
                outlined
                clear-icon="mdi-close-circle"
                rows="3"
                label="Ghi chú "
                v-model="note"

              ></v-textarea>
               <v-checkbox
      v-model="isPrint"
      :label="`In hoá đơn`"
    ></v-checkbox>

              <div>
                <h2>Hình thức thanh toán</h2>
                <v-radio-group v-model="paymentType" mandatory>
                  <v-radio label="Tiền mặt" value="1"></v-radio>
                  <!-- <v-radio
                label="Thanh toán online"
                value="radio-2"
                disabled
              ></v-radio> -->
                </v-radio-group>
              </div>
            </v-col>
            <v-col cols="6">
              <v-row>
                <!-- Chi tiết giỏ hàng -->
                <v-col cols="12">
                  <!-- Món ăn -->
                  <h1>Món ăn</h1>
                  <v-simple-table>
                    <template v-slot:default :fixed-header="true">
                      <thead>
                        <tr>
                          <th class="text-left" :width="180">Tên món</th>
                          <th class="text-left">Số lượng</th>
                          <th class="text-left">Thành tiền</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr v-for="(item, index) in orderInfo" :key="index">
                          <td class="item-cart-name">
                            {{ item.Product.ProductName }}
                          </td>
                          <td>
                            <div>x{{ item.Quantity }}</div>
                          </td>
                          <td>
                            {{
                              formatPrice(
                                item.Product.SellingPrice * item.Quantity
                              )
                            }}đ
                          </td>
                        </tr>
                      </tbody>
                    </template>
                  </v-simple-table>
                </v-col>
                <!-- Thành tiềN -->
                <v-col cols="12">
                  <div style="text-align: right">
                    <div style="margin: 5px 0">
                      <h4>Phí giao hàng: {{ formatPrice(deliveryAmount) }}đ</h4>
                    </div>
                    <h3>Tổng tiền: {{ formatPrice(totalAmount) }}đ</h3>
                  </div>
                </v-col>
                <v-col>
                  <div style="text-align: center">
                    <v-btn outlined color="red" @click="onClickPayment()"
                      >Đặt Hàng Ngay</v-btn
                    >
                  </div>
                </v-col>
              </v-row>
            </v-col>
          </v-row>
        </form>
      </v-container>
      <v-dialog
        transition="dialog-bottom-transition"
        v-model="dialogWarning"
        width="500"
      >
        <v-card>
          <v-toolbar color="warning" dark>Cảnh báo </v-toolbar>

          <v-card-text>
            <div class="text-h5 pa-12">
              Hiện tại, vị trí của bạn đang cách nhà hàng {{ distance }}km. Điều
              này có thể làm ảnh hướng tới chất lượng món ăn. Bạn có muốn tiếp
              tục đặt?
            </div>
          </v-card-text>

          <v-divider></v-divider>

          <v-card-actions class="justify-end">
            <v-btn color="primary" text @click="dialogWarning = false">
              Huỷ
            </v-btn>
            <v-btn
              color="primary"
              text
              @click="
                dialogWarning = false;
                saveOrder();
              "
            >
              TIếp tục
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-main>
  </v-app>
</template>


<script>
import api from "@/services/base/api";
import { validationMixin } from "vuelidate";
import { required, email } from "vuelidate/lib/validators";

export default {
  data() {
    return {
      // Email của người nhận
      email: "",
      // Tên của người nhận
      name: "",
      // Số điện thoại người nhận
      numberPhone: "",
      // Địa chỉ người nhận
      address: "",
      // Ghi chú của người nhậN cho nha hàng
      note: "",
      // Hình thức thanh toán 1-tiền mặt, 2-thẻ hoặc chuyển khoản
      paymentType: 1,
      // Giỏ hàng
      orderInfo: [],
      // Tổng tiền phải trả
      totalAmount: 0,
      // Validate dữ liệu
      validate: true,
      // Số tiền giao hàng
      deliveryAmount: 15000,
      // Vị trí hiệN tại
      myCoordinates: {
        lat: 0,
        lng: 0,
      },
      mapCoordinates: {
        lat: 0,
        lng: 0,
      },
      distance: 0,
      dialogWarning: false,
      result: null,
      isPrint: 0
    };
  },

  mixins: [validationMixin],

  validations: {
    name: { required },
    address: { required },
    email: { required, email },
    numberPhone: { required },
  },

  computed: {
    nameErrors() {
      const errors = [];
      if (!this.$v.name.$dirty) return errors;
      !this.$v.name.required && errors.push("Tên không được để trống!");
      return errors;
    },
    emailErrors() {
      const errors = [];
      if (!this.$v.email.$dirty) return errors;
      !this.$v.email.email && errors.push("Email không hợp lệ");
      !this.$v.email.required && errors.push("E-mail không được để trống");
      return errors;
    },
    numberPhoneErrors() {
      const errors = [];
      if (!this.$v.numberPhone.$dirty) return errors;
      !this.$v.numberPhone.required &&
        errors.push("Số điện thoại không được để trống");
      return errors;
    },
    addressErrors() {
      const errors = [];
      if (!this.$v.address.$dirty) return errors;
      !this.$v.address.required &&
        errors.push("Địa chỉ giao hàng không được để trống");
      return errors;
    },
  },

  created() {
    // Lấy thông tin giỏ hàng
    const orderInfo = localStorage.getItem("cart");
    this.orderInfo = orderInfo ? JSON.parse(orderInfo) : [];
    // Lây thông tin khách hàng đã nhập lần trước
    const customer = JSON.parse(localStorage.getItem("customer"));
    if (customer != null && customer != {}) {
      this.name = customer.Name;
      this.address = customer.Address;
      this.email = customer.Email;
      this.numberPhone = customer.PhoneNumber;
    }
  },

  mounted() {
    this.getMyLocaltion();
    this.calcTotalAmount();
  },

  methods: {
    // Lấy vị trí hiện tại
    getMyLocaltion() {
      this.$getLocation({
        enableHighAccuracy: false, //defaults to false
        timeout: Infinity, //defaults to Infinity
        maximumAge: 0, //defaults to 0
      })
        .then((coordinates) => {
          this.myCoordinates = coordinates;
          this.calcCrow(
            this.myCoordinates.lat,
            this.myCoordinates.lng,
            20.933949,
             106.057133
          );
        })
        .catch((errors) => {
          console.log(errors);
        });
    },

    // Hàm tính tổng tiền
    calcTotalAmount() {
      if (this.orderInfo) {
        this.totalAmount = 0;
        this.orderInfo.forEach((item) => {
          this.totalAmount += item.Product.SellingPrice * item.Quantity;
        });
        this.totalAmount += this.deliveryAmount;
      }
    },

    calcCrow(lat1, lon1, lat2, lon2) {
      var R = 6371; // km
      var myLat = this.toRad(lat2 - lat1);
      var myLon = this.toRad(lon2 - lon1);
      var maplat1 = this.toRad(lat1);
      var maplat2 = this.toRad(lat2);

      var a =
        Math.sin(myLat / 2) * Math.sin(myLat / 2) +
        Math.sin(myLon / 2) *
          Math.sin(myLon / 2) *
          Math.cos(maplat1) *
          Math.cos(maplat2);
      var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
      var d = R * c;
      this.distance = this.convertDamtoKm(d);
      console.log(this.distance);
    },

    // Converts numeric degrees to radians
    toRad(Value) {
      return (Value * Math.PI) / 180;
    },
    // ham lam tròn
    convertDamtoKm(int) {
      var number = int * 1.609344;
      number = Math.round((number + Number.EPSILON) * 100) / 100;
      return number;
    },

    // Thanh toán
    onClickPayment() {
      // Kiểm tra loõi validate
      this.$v.$touch();
      var isValid = true;
      if (this.$v.$invalid) {
        isValid = false;
      }
      let me = this;
      // Kiểm tra trong giỏ hàng có món nào đang quá giới hạn không
      // tạm thời để là true đợi thêm trường thì check
      //var checkKM = this.checkKM();
      var checkKM = true;
      if (isValid) {
        if (checkKM) {
          this.dialogWarning = true;
        } else {
          me.saveOrder();
        }
      } else {
        //Hiển thị thông báo lỗi
        this.$toast.error("Đặt hàng thất bại, vui lòng kiểm tra lại!", {
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
      }
    },

    // Check có món quá số km để cảnh hiển thị cảnh báo
    checkKM() {
      this.orderInfo.forEach((item) => {
        if (item.Product.Distance > this.distance) return true;
      });
      return false;
    },
    //Lưu thông tin người dùng
    setCustomerInfoInLocalStorage() {
      var customer = {
        Name: this.name,
        Address: this.address,
        Email: this.email,
        PhoneNumber: this.numberPhone,
      };
      localStorage.setItem("customer", JSON.stringify(customer));
    },

    //Lưu order
    saveOrder() {
      var me = this;
      var lstDetail = [];
      me.orderInfo.forEach((item) => {
        let orderDetail = {
          ProductId: item.Product.ProductId,
          UnitPrice: item.Product.SellingPrice,
          Quantity: item.Quantity,
          ProductName: item.Product.ProductName
        };
        lstDetail.push(orderDetail);
      });
      let order = {
        OrderCode: me.getNewOrderCode(),
        OrderStatus: 0,
        ReceviedAddress: me.address,
        TelephoneCustomer: me.numberPhone,
        Email: me.email,
        PaymentType: 0,
        Notes: me.note,
        CustomerName: me.name,
        TotalAmount: me.totalAmount,
        ShippingCost: me.deliveryAmount,
        IsPrint: me.isPrint ? 1 : 0,
        OrderDetail: lstDetail
        
      };

      api
        .create("Orders/SaveOrder", order)
        .then((res) => {
          if (res.data.Data) {
            me.clearCart();
            me.setCustomerInfoInLocalStorage();
            //Hiển thị thông báo thành côg
            this.$toast.success("Đặt hàng thành công!", {
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
            this.$router.push("/");
          } else {
            //Hiển thị thông báo lỗi
            this.$toast.error("Đã có lỗi xảy ra. Vui lòng liên hệ nhà hàng!", {
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
          }
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    // clear giỏ hàng
    clearCart() {
      this.orderInfo = [];
      localStorage.setItem("cart", this.orderInfo);
    },
    // Form tiền
    formatPrice(value) {
      let val = (value / 1).toFixed(0).replace(".", ",");
      return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },
    getNewOrderCode() {
      var today = new Date();
      var code = "HD" +
        (today.getDate() < 10 ? "0" : "") +
        today.getDate() +
        (today.getMonth() + 1 < 10 ? "0" : "") +
        (today.getMonth() + 1) +
        today.getFullYear().toString().slice(-2) +
        (today.getHours() < 10 ? "0" : "") +
        today.getHours() +
        (today.getMinutes() < 10 ? "0" : "") +
        today.getMinutes();
      return code;
    },
  },
};
</script>

<style lang="scss" scoped>
</style>