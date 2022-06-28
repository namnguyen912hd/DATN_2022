<template>
  <div>
    <div class="m-content" v-show="isShowDetail == false">
      <div class="m-content-action">
        <div class="m-action-add" @click="onClickBtnAdd()">
          <div class="m-icon m-icon-16 m-add-icon"></div>
          Thêm mới
          <i style="
              padding-left: 35px;
              margin-top: -5px;
              padding-right: 0;
              font-size: 10px;
            " class="fa fa-sort-desc" aria-hidden="true"></i>
        </div>
        <div @click="onClickBtnUpdate()" class="m-action-update">
          <div class="m-icon m-icon-16 m-update-icon"></div>
          Sửa
        </div>
        <div class="m-action-delete" @click="onClickBtnDelete()">
          <!-- :class="{ 'm-disable': selectedProducts.length < 1 }" -->
          <div class="m-icon m-icon-16 m-delete-icon"></div>
          Xóa
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
              <th style="width: 314px">
                <div class="m-thead-content" style="min-width: 112px">
                  <div class="m-thead-name" @click="sorting('ProductCodeSKU', sortOrder)">
                    Mã vai trò
                  </div>

                  <input-filter :column="'ProductCodeSKU'" :valueType="'String'"
                    @onChangeInputValue="onChangeInputValue">
                  </input-filter>
                </div>
              </th>
              <th style="min-width: 299px">
                <div class="m-thead-content" id="thProductName">
                  <div class="m-thead-name" @click="sorting('ProductName', sortOrder)">
                    Tên vai trò
                    <i v-if="isClicked" style="color: #919191; padding-left: 5px" class="fa fa-long-arrow-up"
                      aria-hidden="true"></i>
                    <i v-else style="color: #919191; padding-left: 5px" class="fa fa-long-arrow-down"
                      aria-hidden="true"></i>
                  </div>
                  <input-filter :column="'ProductName'" :valueType="'String'" @onChangeInputValue="onChangeInputValue">
                  </input-filter>
                </div>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="role in roleList" :key="role.RoleId" @click="
              onClickRowActive(
                role.RoleId,
                role.RoleName
              )
            " :class="{ 'row-active': role.RoleId == roleId }">

              <td>{{ role.RoleCode }}</td>
              <td>
                <span class="m-tr-action" @click="onDblClickRow(role.roleId)">{{ role.RoleName }}</span>
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
          <div class="m-icon m-icon-24 m-icon-refresh-24" @click="getAllRole()"></div>
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

    <role-detail :apiRouter="apiRouter" :roleId="roleId" :isShowDetail="isShowDetail" :role="role" :formMode="formMode"
      @hideRoleDetail="hideRoleDetail" @getAllRole="getAllRole">

    </role-detail>
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
import RoleDetail from "../role/RoleDetail.vue"
export default {
  components: {
    BaseLoading,
    InputFilter,
    BasePopup,
    RoleDetail
  },
  data() {
    return {
      money: {
        thousands: ".",
        precision: 0,
      },

      // api
      apiRouter: "roles",
      isClicked: true,

      // biến chứa danh sách role
      roleList: [],
      // id role
      roleId: null,
      // đối tượng role
      role: {
        RoleId: null,
        RoleCode: null,
        RoleName: null,
        LstPermission: null,
      },

      //#region role
      // biến chứa danh sách role
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
      /** mảng chứa role được chọn */
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
      this.getAllRole();
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

  created() {
    this.getAllRole();
  },

  methods: {
    //#region hiển thị dữ liệu
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
        me.getAllRole();
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
        me.getAllRole();
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
        this.getAllRole();
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

    /**
     * lấy dữ liệu role
     * createdBy:: namnguyen(20/12/2022)
     */
    getAllRole() {
      try {
        let me = this;
        let permission = localStorage.getItem('permission');
        if (permission && permission.includes('role_view')) {
          me.isLoading = true;
          // Call api
          setTimeout(function () {
            BaseRequest.get(`${me.apiRouter}/`)
              .then((response) => {
                // gán dữ liệu trả về từ api
                me.roleList = response.data;
                me.totalRecord = parseInt(response.data.length);
                me.totalPage = 1;
                // gán roleId bằng id của bản ghi đầu tiên
                me.roleId = me.roleList[0].RoleId;
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


    //#region CUD
    /**
     * sự kiện click vào btn thêm mới
     * createdBy:namnguyen(18/01/2022)
     */
    onClickBtnAdd() {
      try {
        this.toastMsg.Title = Resourse.ToastMsg.Success.Add;
        this.formMode = Const.FormMode.Add;
        this.isShowDetail = true;
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
        this.toastMsg.Title = Resourse.ToastMsg.Success.Update;
        this.formMode = Const.FormMode.Edit;
        //this.productId = this.selectedProducts[0];
        this.getRoleById(this.roleId);
      } catch (error) {
        console.log(error);
      }
    },

    getRoleById(id) {
      BaseRequest.get(`${this.apiRouter}/${id}`)
        .then((response) => {
          this.role = response.data;
          // Show modal
          this.isShowDetail = true;
        })
        .catch((res) => {
          console.log(res);
        });
    },


    /**
     * sự kiện click vào btn delete
     * createdBy:namnguyen(18/01/2022)
     */
    onClickBtnDelete() {
      try {
        let title = this.titleDelete;
        this.showPopupWarning(title);
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
        BaseRequest.delete(me.apiRouter, me.roleId)
          .then(function () {
            // load lại dữ liệu
            me.getAllRole();
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
    hideRoleDetail() {
      try {
        this.isShowDetail = false;
        // reset dữ liệu hàng hóa
        this.role = {
          roleCode: null,
          roleName: null,
        };
        this.getAllRole();
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
    onClickRowActive(roleId, roleName) {
      this.titleDelete = roleName;
      this.roleId = roleId;
    },
    /**
     * sự kiện double click vào dòng trong table
     * createdBy:namnguyen(18/01/2022)
     */
    onDblClickRow(id) {
      try {
        this.toastMsg.Title = Resourse.ToastMsg.Success.Update;
        this.formMode = Const.FormMode.Edit;
        this.roleId = id;
        this.getRoleById(this.roleId);
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style scoped>
</style>