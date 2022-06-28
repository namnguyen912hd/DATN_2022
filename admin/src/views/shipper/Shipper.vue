<template>
  <div style="height: 100vh">
    <div class="overlay">
      <div style="margin: 30px">
        <h1>Shipper</h1>
        <h1>Đơn hàng hôm nay</h1>
        <div><button @click="getAllOrder()">Làm mới</button></div>
        <table class="m-table">
          <thead class="m-text-left">
            <tr>
              <th>
                <div class="m-thead-content m-thead-custom">
                  <div class="m-thead-name">Mã đơn hàng</div>
                </div>
              </th>
              <th>
                <div class="m-thead-content m-thead-custom">
                  <div class="m-thead-name">Địa chỉ</div>
                </div>
              </th>
              <th>
                <div class="m-thead-content m-thead-custom">
                  <div class="m-thead-name">Người nhận</div>
                </div>
              </th>
              <th>
                <div class="m-thead-content m-thead-custom">
                  <div class="m-thead-name">Số điện thoại</div>
                </div>
              </th>
              <th>
                <div class="m-thead-content m-thead-custom">
                  <div class="m-thead-name">Thao tác</div>
                </div>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="od in orderList" :key="od.OrderId">
              <td>{{ od.OrderCode }}</td>
              <td>
                {{ od.ReceviedAddress }}
              </td>
              <td>
                {{ od.CustomerName }}
              </td>
              <td>{{ od.TelephoneCustomer }}</td>
              <td style="width: 200px">
                <button
                  v-if="od.OrderStatus == 2"
                  @click="doneOrder(od.OrderId, od)"
                  id="btnDone"
                >
                  Hoàn thành
                </button>
                <button
                  v-if="od.OrderStatus == 2"
                  @click="cancelOrder(od.OrderId, od)"
                  id="btnCancel"
                >
                  Hủy
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
import BaseRequest from "@/apis/baseRequest";
export default {
  components: {},
  created() {
    this.getAllOrder();
  },
  data() {
    return {
      // api
      apiRouter: "Orders",
      //#region product
      // biến chứa danh sách order
      orderList: [],
      // id order
      oderId: null,
      // đối tượng order
      order: {
        OrderCode: null,
        OrderDate: null,
        OrderStatus: null,
        Email: null,
        ShippingCost: null,
        ReceviedAddress: null,
        TelephoneCustomer: null,
        PaymentType: null,
        Notes: null,
        OrderDetail: null,
      },
    };
  },
  methods: {
    getAllOrder() {
      try {
        let me = this;
        // Call api
        setTimeout(function () {
          BaseRequest.get(`Orders/OrderForShipper`)
            .then((response) => {
              // gán dữ liệu trả về từ api
              me.orderList = response.data;
            })
            .catch((e) => {
              me.isLoading = false;
              console.log(e);
            });
        }, 1000);
      } catch (error) {
        console.log(error);
      }
    },
    doneOrder(id, order) {
      try {
        let me = this;
        me.order = order;
        me.order.OrderStatus = 3;
        BaseRequest.put(me.apiRouter + "/" + id, me.order)
          .then(() => {
            alert("Đơn hàng thành công");
            me.getAllOrder();
          })
          .catch(function (res) {
            console.log(res.response.data);
          });
      } catch (error) {
        console.log(error);
      }
    },
    cancelOrder(id, order) {
      try {
        let me = this;
        me.order = order;
        me.order.OrderStatus = 4;
        BaseRequest.put(me.apiRouter + "/" + id, me.order)
          .then(() => {
            alert("Đơn hàng đã hủy");
            me.getAllOrder();
          })
          .catch(function (res) {
            console.log(res.response.data);
          });
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style scoped>
.overlay {
  position: absolute;
  top: 0px;
  right: 0px;
  left: 0;
  bottom: 0;
  background-color: #f4f5f8;
  opacity: 1;
  z-index: 1400;
}
.m-info-custom {
  font-size: 15px;
  min-width: 180px;
  padding: 4px;
}
.m-thead-custom {
  height: 24px;
  border-bottom: none;
}
#btnDone {
  padding: 5px;
  background-color: rgb(27, 173, 57);
  font-size: 15px;
  border: 1px solid rgb(27, 173, 57);
  border-radius: 3px;
  color: white;
  margin-right: 6px;
  cursor: pointer;
}
#btnCancel {
  padding: 5px;
  background-color: rgb(203, 54, 54);
  font-size: 15px;
  border: 1px solid rgb(203, 54, 54);
  border-radius: 3px;
  color: white;
  width: 69px;
  cursor: pointer;
}
</style>