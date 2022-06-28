<template>
  <div>
    <div class="m-content" v-show="isShowDetail == false">
      <div class="m-content-action">

        <div @click="onClickBtnUpdate()" class="m-action-update">
          <div class="m-icon m-icon-16 m-update-icon"></div>
          Sửa
        </div>
        <div class="m-action-refresh" @click="btnRefreshOnClick()">
          <div class="m-icon m-icon-16 m-refresh-icon"></div>
          Nạp
        </div>
      </div>

      <div class="m-content-grid">
        <!-- --table -->
        <table class="m-table">
          <thead class="m-text-left">
            <tr>
              <th style="min-width: 114px">
                <div class="m-thead-content" style="min-width: 112px">
                  <div class="m-thead-name" @click="sorting('OrderData', sortOrder)">
                    Thời gian tạo
                  </div>

                  <input-filter :column="'OrderData'" :valueType="'String'"
                    @onChangeInputValue="onChangeInputValue">
                  </input-filter>
                </div>
              </th>
              <th style="min-width: 114px">
                <div class="m-thead-content" style="min-width: 112px">
                  <div class="m-thead-name" @click="sorting('ProductCodeSKU', sortOrder)">
                    Mã đơn hàng
                  </div>

                  <input-filter :column="'ProductCodeSKU'" :valueType="'String'"
                    @onChangeInputValue="onChangeInputValue">
                  </input-filter>
                </div>
              </th>
              <th style="min-width: 179px">
                <div class="m-thead-content" id="thProductName">
                  <div class="m-thead-name" @click="sorting('ProductName', sortOrder)">
                    Người nhận
                    <i v-if="isClicked" style="color: #919191; padding-left: 5px" class="fa fa-long-arrow-up"
                      aria-hidden="true"></i>
                    <i v-else style="color: #919191; padding-left: 5px" class="fa fa-long-arrow-down"
                      aria-hidden="true"></i>
                  </div>
                  <input-filter :column="'ProductName'" :valueType="'String'" @onChangeInputValue="onChangeInputValue">
                  </input-filter>
                </div>
              </th>
              <th style="min-width: 299px">
                <div class="m-thead-content" id="thReceviedAddress">
                  <div class="m-thead-name" @click="sorting('ReceviedAddress', sortOrder)">
                    Địa chỉ
                    <i v-if="isClicked" style="color: #919191; padding-left: 5px" class="fa fa-long-arrow-up"
                      aria-hidden="true"></i>
                    <i v-else style="color: #919191; padding-left: 5px" class="fa fa-long-arrow-down"
                      aria-hidden="true"></i>
                  </div>
                  <input-filter :column="'ReceviedAddress'" :valueType="'String'" @onChangeInputValue="onChangeInputValue">
                  </input-filter>
                </div>
              </th>
              <th style="min-width: 114px">
                <div class="m-thead-content">
                  <div class="m-thead-name" @click="sorting('orderName', sortOrder)">
                    Số điện thoại
                  </div>
                  <input-filter :column="'orderName'" :valueType="'String'" @onChangeInputValue="onChangeInputValue">
                  </input-filter>
                </div>
              </th>
              <th>
                <div class="m-thead-content">
                  <div class="m-thead-name" @click="sorting('CalculationName', sortOrder)">
                    Kiểu thanh toán
                  </div>
                  <input-filter :column="'CalculationName'" :valueType="'String'"
                    @onChangeInputValue="onChangeInputValue">
                  </input-filter>
                </div>
              </th>
              <th>
                <div class="m-thead-content">
                  <div class="m-thead-name" @click="sorting('CalculationName', sortOrder)">
                    Tình trạng
                  </div>
                  <input-filter :column="'CalculationName'" :valueType="'String'"
                    @onChangeInputValue="onChangeInputValue">
                  </input-filter>
                </div>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="cal in orderList" :key="cal.OderId"  >
              <td>{{ formatDateTime(cal.OrderDate) }}</td>
              <td>{{ cal.OrderCode }}</td>
              <td>
                <span class="m-tr-action" @click="onDblClickRow(cal.OrderId)">{{ cal.CustomerName }}</span>

              </td>
              <td>{{ cal.ReceviedAddress }}</td>
              <td>{{ cal.TelephoneCustomer }}</td>
              <td>
                <div v-if="cal.PaymentType==0">Tiền mặt</div>
                <div v-if="cal.PaymentType==1">Chuyển khoản</div>
                </td>
              <td>
                <div v-if=" cal.OrderStatus == 0">Chưa xác nhận</div>
                <div v-if=" cal.OrderStatus == 1">Đã xác nhận</div>
                <div v-if=" cal.OrderStatus == 2">Đang giao hàng</div>
                <div v-if=" cal.OrderStatus == 3">Hoàn thành</div>
                <div v-if=" cal.OrderStatus == 4">Huỷ</div>
                
                </td>
            </tr>
          </tbody>
        </table>
      </div>

      <div class="m-content-pagination">
        <div class="m-pagination-left">
          <div class="m-icon m-icon-24 m-icon-first" :class="{ 'm-disable': pageIndex < 2 }" @click="pageIndex = 1">
          </div>
          <div class="m-icon m-icon-24 m-icon-prev" :class="{ 'm-disable': pageIndex < 2 }" @click="pageIndex--"></div>
          <div class="m-input-pagesize">
            Trang
            <input class="m-input" v-mask="'#####'" type="text" v-model="txtPageIndex"
              @keyup.enter="changeInputPageIndex()" />
            trên {{ totalPage }}
          </div>
          <div class="m-icon m-icon-24 m-icon-next" :class="{ 'm-disable': pageIndex >= totalPage }"
            @click="pageIndex++"></div>
          <div class="m-icon m-icon-24 m-icon-end" :class="{ 'm-disable': pageIndex >= totalPage }"
            @click="pageIndex = totalPage"></div>
          <div class="m-icon m-icon-24 m-icon-refresh-24" @click="getAllOrder()"></div>
          <div class="m-pagesize-container" style="position: relative">
            <div class="m-select-pagesize" @click="showPagesize()">
              <div class="m-display-pagesize">
                <span style="width: 22px">{{ pageSize }}</span>
                <i class="fa fa-angle-down" aria-hidden="true"></i>
              </div>
            </div>
            <div class="m-option-pagesize" v-if="isShowPageSize">
              <div class="m-option" @click="selectPagesize(100)">100</div>
              <div class="m-option" @click="selectPagesize(50)">50</div>
              <div class="m-option" @click="selectPagesize(30)">30</div>
              <div class="m-option" @click="selectPagesize(15)">15</div>
            </div>
          </div>
        </div>
        <div class="m-pagination-right">
          <div v-if="totalRecord > 0">
            Hiển thị {{ pageIndex * pageSize - pageSize + 1 }} -
            {{
                pageSize * pageIndex > totalRecord
                  ? totalRecord
                  : pageSize * pageIndex
            }}
            trên {{ totalRecord }} kết quả
          </div>
        </div>
      </div>
      <base-loading :isLoading="isLoading"></base-loading>
    </div>
    <!-- pop up -->
    <base-popup :isShowPopup="isShowPopup" :popup="popup" @onClickClosePopup="hidePopup" @deleteProduct="deleteProduct">
    </base-popup>

    <!-- form product -->
    <order-detail :apiRouter="apiRouter" :orderId="oderId" :isShowDetail="isShowDetail" :order="order"
      :formMode="formMode" @hideOrderDetail="hideOrderDetail" @getAllOrder="getAllOrder">
    </order-detail>
  </div>
</template>

<script>
import BaseRequest from "@/apis/baseRequest";
import BaseLoading from "@/components/base/BaseLoading.vue";
import Enum from "@/commons/enum";
import Const from "@/commons/const";
import Resourse from "@/commons/resource";
import InputFilter from "@/components/base/InputFilter.vue";
import BasePopup from "@/components/base/BasePopup.vue";
import OrderDetail from "../order/OrderDetail.vue";
export default {
  components: {
    BaseLoading,
    InputFilter,
    BasePopup,
    OrderDetail
  },
  data() {
    return {
      money: {
        thousands: ".",
        precision: 0,
      },

      // api
      apiRouter: "Orders",
      isClicked: true,

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

      isShowPageSize: false,
      titleDelete: "",

      pageSize: 50,
      pageIndex: 1,
      totalPage: 1,
      totalRecord: 0,
      txtPageIndex: 1,
      /* Đối tượng tìm kiếm */
      objFilter: {},
      /* Danh sách đổi tượng tìm kiếm */
      listObjFilters: [],
      // đối tượng để sắp xếp dữ liệu
      objSort: {
        Column: null,
        SortOrder: Enum.Sort.Asc,
      },
      sortOrder: Enum.Sort.Asc,

      isShowDetail: false,
      formMode: null,

      //#endregion

      //#region toast msg + checkbox

      // toast message
      isShowToastMsg: false,
      toastMsg: {
        Title: "",
        Type: "",
      },
      /**
       * btn check all ở thead
       */
      isCheckAll: false,
      /** mảng chứa order được chọn */
      selectedProducts: [],

      //#endregion

      isLoading: false,

      // Popup
      isShowPopup: false,
      popup: {
        Title: "",
        Status: "",
      },
    };
  },

  watch: {
    pageIndex() {
      this.getAllOrder();
      this.txtPageIndex = this.pageIndex;
    },

    showValue() {
      let objFilter = {
        Column: "ShowStatus",
        Value: this.showValue,
        Operator: Enum.Operator.EqualTo,
        ValueType: Const.ValueType.Int,
        AdditionalOperator: Enum.AdditionalOperator.And,
      };
      this.onChangeInputValue(objFilter);
    },
    stateValue() {
      let objFilter = {
        Column: "BusinessStatus",
        Value: this.stateValue,
        Operator: Enum.Operator.EqualTo,
        ValueType: Const.ValueType.Int,
        AdditionalOperator: Enum.AdditionalOperator.And,
      };
      this.onChangeInputValue(objFilter);
    },
  },

  created() {
    this.getAllOrder();
  },

  methods: {
    //#region hiển thị dữ liệu

    /**
     * lấy dữ liệu order
     * createdBy:: namnguyen(20/12/2022)
     */
    getAllOrder() {
      try {
        let me = this;
        let permission = localStorage.getItem('permission');
        if (permission && permission.includes('order_view')) {
          // hiện loading
          me.isLoading = true;
          // Call api
          setTimeout(function () {
            BaseRequest.get(`${me.apiRouter}/`)
              .then((response) => {
                // gán dữ liệu trả về từ api
                me.orderList = response.data;
                me.totalRecord = parseInt(response.data.length);
                me.totalPage = 1;
                // gán oderId bằng id của bản ghi đầu tiên
                me.oderId = me.orderList[0].OrderId;

                // ẩn loading
                me.isLoading = false;
              })
              .catch((e) => {
                me.isLoading = false;
                console.log(e);
              });
          }, 1000);
        } else {
          window.location.href = "http://localhost:8080/unauthen";
        }

      } catch (error) {
        console.log(error);
      }
    },

    /**
     * hàm sắp xếp dữ liệu
     * createdBy: namnguyen(24/01/2022)
     */
    sorting(columnName, sortOrder) {
      try {
        let me = this;
        me.objSort.Column = columnName;
        if (sortOrder == Enum.Sort.Asc) {
          me.objSort.SortOrder = Enum.Sort.Asc;
          me.sortOrder = Enum.Sort.Desc;
        } else {
          me.objSort.SortOrder = Enum.Sort.Desc;
          me.sortOrder = Enum.Sort.Asc;
        }
        me.getAllOrder();
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * hàm filter dữ liệu
     * createdBy: namnguyen(26/01/2022)
     */
    onChangeInputValue(objFilter) {
      try {
        let me = this;
        me.listObjFilters = me.listObjFilters || [];
        if (objFilter.Value === "" || objFilter.Value == null) {
          const index = me.listObjFilters.findIndex(
            (f) => f.Column == objFilter.Column
          );
          me.listObjFilters.splice(index, 1);
        }
        // obj có giá trị
        else {
          const filter = me.listObjFilters.find(
            (f) => f.Column == objFilter.Column
          );
          if (filter) {
            filter.Value = objFilter.Value;
            filter.Operator = objFilter.Operator;
          } else {
            me.listObjFilters.push(objFilter);
          }
        }
        me.getAllOrder();
      } catch (error) {
        console.log(error);
      }
    },

    // ==========================================================================================

    /**
     * hàm làm mới dữ liệu
     * createdBy: namnguyen(24/01/2022)
     */
    btnRefreshOnClick() {
      try {
        this.getAllOrder();
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * hàm hiển thị dropdown số trang
     * createdBy: namnguyen(18/01/2022)
     */
    showPagesize() {
      try {
        this.isShowPageSize = !this.isShowPageSize;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * hàm chọn số trang
     * createdBy:: namnguyen(25/01/2022)
     */
    selectPagesize() {
      try {
        console.log(1123);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * hàm thay đổi input số trang
     * createdBy:: namnguyen(25/01/2022)
     */
    changeInputPageIndex() {
      try {
        console.log(1123);
      } catch (error) {
        console.log(error);
      }
    },

    //#endregion

    //#region CUD

    /**
     * hiển thị thống báo khi có lỗi sảy ra
     * param {title} nội dung thông báo
     * createdBy: namnguyen (19/01/2022)
     */
    showPopupDanger(title) {
      try {
        this.isShowPopup = true;
        this.popup.Status = Resourse.PopUp.Status.Danger;
        this.popup.Title = title;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * sự kiện click vào btn cập nhập
     * createdBy:namnguyen(18/01/2022)
     */
    onClickBtnUpdate() {
      try {
        let permission = localStorage.getItem('permission');
        if (permission && permission.includes('order_edit')) {
          this.toastMsg.Title = Resourse.ToastMsg.Success.Update;
          this.formMode = Const.FormMode.Edit;
          //this.productId = this.selectedProducts[0];
          this.getOrderById(this.oderId);
        }
        else {
          this.showPopupDanger('Bạn không có quyền thực hiện thao tác này');
        }


      } catch (error) {
        console.log(error);
      }
    },

    getOrderById(id) {
      this.orderId = id;
      let permission = localStorage.getItem('permission');
      if (permission && permission.includes('order_edit')) {
        BaseRequest.get(`${this.apiRouter}/${id}`)
          .then((response) => {
            this.order = response.data;
            // Show modal
            this.isShowDetail = true;
          })
          .catch((res) => {
            console.log(res);
          });
      }
      else {
        this.showPopupDanger('Bạn không có quyền thực hiện thao tác này');
      }
    },


    /**
     * sự kiện click vào btn delete
     * createdBy:namnguyen(18/01/2022)
     */
    onClickBtnDelete() {
      try {

        let permission = localStorage.getItem('permission');
        if (permission && permission.includes('order_delete')) {
          let title = this.titleDelete;
          this.showPopupWarning(title);
        }
        else {
          this.showPopupDanger('Bạn không có quyền thực hiện thao tác này');
        }

      } catch (error) {
        console.log(error);
      }
    },
    showPopupWarning(title) {
      try {
        this.isShowPopup = true;
        this.popup.Status = Resourse.PopUp.Status.Warning;
        this.popup.Title = title;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * sự kiện click button xác nhận xóa
     * createdBy:: namnguyen (19/01/2022)
     */
    deleteProduct() {
      try {
        // Call api
        var me = this;
        BaseRequest.delete(me.apiRouter, me.oderId)
          .then(function () {
            // load lại dữ liệu
            me.getAllOrder();
            // hiện toast message
            me.toastMsg.Title = Resourse.ToastMsg.Success.Delete;
            setTimeout(() => {
              me.showToastMsg();
            }, 100);
            me.hideToastMsg();
            // ẩn popup
            me.hidePopup(false);
            me.selectedProducts = [];
            this.isCheckAll = !this.isCheckAll;
          })
          .catch(function (e) {
            console.log(e);
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * hiển thị toast message
     * createdBy: namnguyen (23/01/2022)
     */
    showToastMsg() {
      try {
        this.isShowToastMsg = true;
        this.toastMsg.Type = Resourse.ToastMsg.Type.Success;
        setTimeout(this.hideToastMsg, 2500);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * ẩn form chi tiết hàng hóa
     * createdBy:: namnguyen(25/01/2022)
     */
    hideOrderDetail() {
      try {
        this.getAllOrder();
        this.isShowDetail = false;
        // reset dữ liệu hàng hóa
        this.order = {
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
        };
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * ẩn pop-up
     * createdBy: namnguyen (19/01/2022)
     */
    hidePopup() {
      try {
        this.isShowPopup = false;
        this.popup.Status = "";
        this.popup.Title = "";
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * ẩn toast message
     */
    hideToastMsg() {
      this.isShowToastMsg = false;
    },
    onClickRowActive(oderId, orderCode) {
      this.titleDelete = orderCode;
      this.oderId = oderId;
    },
    /**
     * sự kiện double click vào dòng trong table
     * createdBy:namnguyen(18/01/2022)
     */
    onDblClickRow(id) {
      try {
        this.toastMsg.Title = Resourse.ToastMsg.Success.Update;
        this.formMode = Const.FormMode.Edit;
        this.oderId = id;
        this.getOrderById(this.oderId);
      } catch (error) {
        console.log(error);
      }
    },

    formatDateTime(datetime)
    {
      var today = new Date(datetime);
      var time = 
        (today.getDate() < 10 ? "0" : "") +
        today.getDate() + "/" +
        (today.getMonth() + 1 < 10 ? "0" : "") +
        (today.getMonth() + 1) +"/" +
        today.getFullYear().toString() + " " +
        (today.getHours() < 10 ? "0" : "") +
        today.getHours() + ":" +
        (today.getMinutes() < 10 ? "0" : "") +
        today.getMinutes();
      return time;
    
    }
  },
};
</script>

<style scoped>
</style>