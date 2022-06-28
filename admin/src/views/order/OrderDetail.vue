<template>
  <div>
    <div class="m-content m-content-detail" v-if="isShowDetail">
      <div
        class="m-content-action m-detail-action"
        style="border-bottom: 1px solid #ddd"
      >
        <button
          style="width: 55px"
          v-if="order.IsPrint == 1"
          class="m-btn m-btn-save"
          @click="printPDF()"
        >
          <div class="m-icon m-icon-16 m-icon-save"></div>
          In
        </button>
      </div>

      <div class="m-content-grid m-detail-info">
        <form id="contentpdf">
          <div class="m-info m-info-basic">
            <div class="m-title">Thông tin cơ bản</div>

            <div class="m-info-item">
              <div class="m-info-name m-info-custom">Mã đơn hàng:</div>
              <div class="m-info-content">
                {{ order.OrderCode }}
              </div>
            </div>

            <div class="m-info-item">
              <div class="m-info-name m-info-custom">Người nhận:</div>
              <div class="m-info-content">
                {{ order.CustomerName }}
              </div>
            </div>

            <div class="m-info-item">
              <div class="m-info-name m-info-custom">Địa chỉ nhận:</div>
              <div class="m-info-content">
                {{ order.ReceviedAddress }}
              </div>
            </div>

            <div class="m-info-item">
              <div class="m-info-name m-info-custom">Số điện thoại:</div>
              <div class="m-info-content">
                {{ order.TelephoneCustomer }}
              </div>
            </div>

            <div class="m-info-item">
              <div class="m-info-name m-info-custom">Phí ship:</div>
              <div class="m-info-content">15.000đ</div>
            </div>

            <div class="m-info-item">
              <div class="m-info-name m-info-custom">Tổng tiền:</div>
              <div class="m-info-content">
                {{ formatPrice(order.TotalAmount) }}đ
              </div>
            </div>

            <div
              class="m-info-item"
            >
              <div class="m-info-name m-info-custom">Tình trạng:</div>
              <div class="m-info-content">
                <v-select
                  v-model="order.OrderStatus"
                  :reduce="(option) => option.value"
                  :options="optionOrderStatus"
                >
                </v-select>
              </div>
            </div>

            <div
              class="m-info-item"
              v-if="order.OrderStatus == 2 && isDelivery"
            >
              <div class="m-info-name m-info-custom">Shipper:</div>
              <div class="m-info-content">
                <v-select
                  v-model="order.ShipperName"
                  :reduce="(UserName) => UserName.UserName"
                  label="UserName"
                  :options="optionShipper"
                >
                </v-select>
              </div>
            </div>
          </div>

          <div class="m-info m-info-basic">
            <div class="m-title">Chi tiết sản phẩm</div>
            <table class="m-table">
              <thead class="m-text-left">
                <tr>
                  <th>
                    <div class="m-thead-content m-thead-custom">
                      <div class="m-thead-name">Tên sản phẩm</div>
                    </div>
                  </th>
                  <th>
                    <div class="m-thead-content m-thead-custom">
                      <div class="m-thead-name">Đơn giá</div>
                    </div>
                  </th>
                  <th>
                    <div class="m-thead-content m-thead-custom">
                      <div class="m-thead-name">Số lượng</div>
                    </div>
                  </th>
                  <th>
                    <div class="m-thead-content m-thead-custom">
                      <div class="m-thead-name">Thành tiền</div>
                    </div>
                  </th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="od in order.OrderDetail" :key="od.OderDetailId">
                  <td>{{ od.ProductName }}</td>
                  <td class="m-text-right">
                    {{ od.UnitPrice | formatNumber2Currency }}
                  </td>
                  <td class="m-text-right">{{ od.Quantity }}</td>
                  <td class="m-text-right">
                    {{ (od.UnitPrice * od.Quantity) | formatNumber2Currency }}
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </form>
      </div>

      <div
        class="m-content-action m-detail-action"
        style="border-bottom: 1px solid #ddd"
      >
        <button
          class="m-btn m-btn-save"
          @click="onClickSubmit()"
        >
          <div class="m-icon m-icon-16 m-icon-save"></div>
          Lưu
        </button>
        <button class="m-btn m-btn-cancel" @click="hideFormData()">
          <div class="m-icon m-icon-16 m-icon-cancel"></div>
          Hủy bỏ
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import BaseRequest from "@/apis/baseRequest";
import { required } from "vuelidate/lib/validators";
import Const from "@/commons/const";
// import Enum from "@/commons/enum";
// import Resource from "@/commons/resource";

export default {
  components: {},
  props: ["isShowDetail", "formMode", "order", "orderId", "apiRouter"],

  data() {
    return {
      // chế độ submit (true: submit, false: không submit)
      submitted: false,
      optionOrderStatus: [
        { label: "Chưa xác nhận", value: 0 },
        { label: "Đã xác nhận", value: 1 },
        { label: "Đang giao", value: 2 },
      ],
      isDelivery: false,
      optionShipper: [],
    };
  },
  filters: {
    /**
     * chuyển dạng số sang tiền tệ
     * createdBy:: namnguyen(20/12/2022)
     */
    formatNumber2Currency(number) {
      if (number) {
        // convert to string
        let result = number + "";
        result = result.replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        return result;
      }
      return "";
    },
  },
  created() {},

  mounted() {
    
  },
  // kiểm tra dữ liệu
  validations: {
    user: {
      UserName: {
        required,
      },
    },
  },
  methods: {
    getAllShipper() {
      try {
        BaseRequest.get("Users/GetAllShipper")
          .then((res) => {
            this.optionShipper = res.data;
          })
          .catch(function (res) {
            console.log(res.response);
          });
      } catch (error) {
        console.log(error);
      }
    },
    formatPrice(value) {
      let val = (value / 1).toFixed(0).replace(".", ",");
      return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },
    getDateTimeNow() {
      var today = new Date();
      var time =
        today.getFullYear().toString() +
        "-" +
        (today.getMonth() + 1 < 10 ? "0" : "") +
        (today.getMonth() + 1) +
        "-" +
        (today.getDate() < 10 ? "0" : "") +
        today.getDate() +
        "T" +
        (today.getHours() < 10 ? "0" : "") +
        today.getHours() +
        ":" +
        (today.getMinutes() < 10 ? "0" : "") +
        today.getMinutes() +
        ":" +
        (today.getSeconds() < 10 ? "0" : "") +
        today.getSeconds();
      return time;
    },
    /**
     * ẩn form
     * createdBy: namnguyen(19/01/2022)
     */
    hideFormData() {
      try {
        this.submitted = false;
        this.$emit("hideOrderDetail");
      } catch (error) {
        console.log(error);
      }
    },

    //#region save data

    /**
     * sự kiện click vào submit button
     * createdBy: namnguyen(24/01/2022)
     */
    onClickSubmit() {
      try {
        let me = this;
        me.submitted = true;
        // validate dữ liệu
        if (this.formMode == Const.FormMode.Add) {
          BaseRequest.post(me.apiRouter, me.user)
            .then(() => {
              this.$emit("getAllOrder");
              me.submitted = false;
              me.hideFormData();
            })
            .catch(function (res) {
              console.log(res.response);
            });
        } else {
          // update data
          if (me.order.OrderStatus == 1) {
            me.order.UserComfirmName = "nvnam2";
            me.order.ConfirmationTime = me.getDateTimeNow();
          }
          if (me.order.OrderStatus == 2) {
            me.order.DeliveryTime = me.getDateTimeNow();
          }
          BaseRequest.put(me.apiRouter + "/" + me.orderId, me.order)
            .then(() => {
              this.$emit("getAllOrder");
              me.submitted = false;
              me.hideFormData();
            })
            .catch(function (res) {
              console.log(res.response.data);
            });
        }
      } catch (error) {
        console.log(error);
      }
    },

    deleteOrder() {
      try {
        // Call api
        var me = this;
        BaseRequest.delete(me.apiRouter, me.orderId)
          .then(function () {
            this.$emit("getAllOrder");
            me.submitted = false;
            me.hideFormData();
          })
          .catch(function (e) {
            console.log(e);
          });
      } catch (error) {
        console.log(error);
      }
    },
    printPDF() {
      // window.html2canvas = html2canvas;
      // //document.getElementById(id).style.property = new style
      // let doc  = new jsPDF("p","pt","a4");
      // doc.html(document.querySelector("#contentpdf"),{
      //   callback: function(pdf){
      //     pdf.save("sainvoice.pdf")
      //   }
      // })
      // Get HTML to print from element
      let prtHtml = document.querySelector("#contentpdf").innerHTML;
      // Get all stylesheets HTML
      let stylesHtml = "";
      for (const node of [
        ...document.querySelectorAll('link[rel="stylesheet"], style'),
      ]) {
        stylesHtml += node.outerHTML;
      }
      // Open the print window
      const WinPrint = window.open(
        "",
        "",
        "left=0,top=0,width=800,height=900,toolbar=0,scrollbars=0,status=0"
      );
      WinPrint.document.write(`<!DOCTYPE html>
                              <html>
                                <head>
                                  ${stylesHtml}
                                </head>
                                <body>
                                  ${prtHtml}
                                </body>
                              </html>`);
      WinPrint.document.close();
      WinPrint.focus();
      WinPrint.print();
      //WinPrint.close();
    },
    //#endregion
  },
  directives: {
    focus: {
      inserted: (el) => {
        el.focus();
      },
    },
  },
  watch: {
    isShowDetail() {
      if (this.isShowDetail) {
        if (this.order.OrderStatus == 0) {
          this.optionOrderStatus = [
            { label: "Chưa xác nhận", value: 0 },
            { label: "Đã xác nhận", value: 1 },
          ];
        }
        if (this.order.OrderStatus == 1) {
          this.isDelivery = true;
          this.getAllShipper();
          this.optionOrderStatus = [
            { label: "Đã xác nhận", value: 1 },
            { label: "Đang Giao", value: 2 },
          ];
        }
        if (this.order.OrderStatus == 2) {
          this.isDelivery = true;
          this.getAllShipper();
          this.optionOrderStatus = [
            { label: "Đang Giao", value: 2 },
          ];
        }
      }
    },
  },
};
</script>

<style lang="css" scoped>
.m-info-custom {
  font-size: 15px;
  min-width: 180px;
  padding: 4px;
}

.m-thead-custom {
  height: 24px;
  border-bottom: none;
}
</style>