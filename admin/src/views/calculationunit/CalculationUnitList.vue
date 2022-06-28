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
        <div class="m-action-clone" @click="onClickBtnClone()">
          <div class="m-icon m-icon-16 m-clone-icon"></div>
          Nhân bản
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
              <th style="min-width: 17px">
                <label class="m-checkbox-input">
                  <input type="checkbox" @click="onClickCheckAll()" v-model="isCheckAll" />
                  <span class="m-checkbox-checked"></span>
                </label>
              </th>
              <th style="min-width: 114px">
                <div class="m-thead-content" style="min-width: 112px">
                  <div class="m-thead-name" @click="sorting('ProductCodeSKU', sortOrder)">
                    Mã đơn vị tính
                  </div>

                  <input-filter :column="'ProductCodeSKU'" :valueType="'String'"
                    @onChangeInputValue="onChangeInputValue">
                  </input-filter>
                </div>
              </th>
              <th style="min-width: 299px">
                <div class="m-thead-content" id="thProductName">
                  <div class="m-thead-name" @click="sorting('ProductName', sortOrder)">
                    Tên Đơn vị tính
                    <i v-if="isClicked" style="color: #919191; padding-left: 5px" class="fa fa-long-arrow-up"
                      aria-hidden="true"></i>
                    <i v-else style="color: #919191; padding-left: 5px" class="fa fa-long-arrow-down"
                      aria-hidden="true"></i>
                  </div>
                  <input-filter :column="'ProductName'" :valueType="'String'" @onChangeInputValue="onChangeInputValue">
                  </input-filter>
                </div>
              </th>
              <th style="min-width: 114px">
                <div class="m-thead-content">
                  <div class="m-thead-name" @click="sorting('ProductCategoryName', sortOrder)">
                    Mô tả
                  </div>
                  <input-filter :column="'ProductCategoryName'" :valueType="'String'"
                    @onChangeInputValue="onChangeInputValue">
                  </input-filter>
                </div>
              </th>
              <th>
                <div class="m-thead-content">
                  <div class="m-thead-name" @click="sorting('CalculationName', sortOrder)">
                    Người tạo
                  </div>
                  <input-filter :column="'CalculationName'" :valueType="'String'"
                    @onChangeInputValue="onChangeInputValue">
                  </input-filter>
                </div>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="cal in calculationUnitList" :key="cal.CalculationUnitId">
              <td>
                <label class="m-checkbox-input">
                  <input type="checkbox" v-model="selectedProducts" />
                  <span class="m-checkbox-checked"></span>
                </label>
              </td>
              <td>{{ cal.CalculationUnitCode }}</td>
              <td>
                <span class="m-tr-action">{{ cal.CalculationUnitName }}</span>
              </td>
              <td>{{ cal.Description }}</td>
              <td>{{ cal.CreatedBy }}</td>
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
          <div class="m-icon m-icon-24 m-icon-refresh-24" @click="getAllCalculationUnit()"></div>
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

  </div>
</template>

<script>
import BaseRequest from "@/apis/baseRequest";
import BaseLoading from "@/components/base/BaseLoading.vue";
import Enum from "@/commons/enum";
import Const from "@/commons/const";
import InputFilter from "@/components/base/InputFilter.vue";
export default {
  components: {
    BaseLoading,
    InputFilter,
  },
  data() {
    return {
      money: {
        thousands: ".",
        precision: 0,
      },

      // api
      apiRouter: "CalculationUnits",
      isClicked: true,

      //#region product
      // biến chứa danh sách hàng hóa
      calculationUnitList: [],
      // id hàng hóa
      calculationUnitId: "",
      // đối tượng hàng hóa
      calculationUnit: {
        CalculationUnitCode: null,
        CalculationUnitName: null,
        Description: null,
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
      /** mảng chứa hàng hóa được chọn */
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
      this.getAllCalculationUnit();
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
    this.getAllCalculationUnit();
  },

  methods: {
    //#region hiển thị dữ liệu

    /**
     * lấy dữ liệu hàng hóa
     * createdBy:: namnguyen(20/12/2022)
     */
    getAllCalculationUnit() {
      try {
        let me = this;
        // hiện loading
        me.isLoading = true;
        let permission = localStorage.getItem('permission');
        if (permission && permission.includes('calculationunit_view')) {
          // Call api
          setTimeout(function () {
            BaseRequest.get(`${me.apiRouter}/`)
              .then((response) => {
                // gán dữ liệu trả về từ api
                me.calculationUnitList = response.data;
                me.totalRecord = parseInt(response.data.length);
                me.totalPage = 1;
                // ẩn loading
                me.isLoading = false;
              })
              .catch((e) => {
                me.isLoading = false;
                console.log(e);
              });
          }, 1000);         
        }
        else {
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
        me.getAllCalculationUnit();
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
        me.getAllCalculationUnit();
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
        console.log(1123);
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
     * ẩn form chi tiết hàng hóa
     * createdBy:: namnguyen(25/01/2022)
     */
    hideProductDetail() {
      try {
        console.log(1123);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * sự kiện click vào btn thêm mới
     * createdBy:namnguyen(18/01/2022)
     */
    onClickBtnAdd() {
      try {
        console.log(1123);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * sự kiện click vào btn nhân bản
     * createdBy:namnguyen(18/01/2022)
     */
    onClickBtnClone() {
      try {
        console.log(1123);
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
        console.log(1123);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * sự kiện click vào btn delete
     * createdBy:namnguyen(18/01/2022)
     */
    onClickBtnDelete() {
      try {
        console.log(1123);
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style scoped>
</style>