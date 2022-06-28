<template>
  <div>
    <div class="m-content m-content-detail" v-if="isShowDetail">
      <div
        class="m-content-action m-detail-action"
        style="border-bottom: 1px solid #ddd"
      >
        <button class="m-btn m-btn-save" @click="onClickSubmit()">
          <div class="m-icon m-icon-16 m-icon-save"></div>
          Lưu
        </button>
        <button class="m-btn m-btn-cancel" @click="hideFormData()">
          <div class="m-icon m-icon-16 m-icon-cancel"></div>
          Hủy bỏ
        </button>
      </div>

      <div class="m-content-grid m-detail-info">
        <form>
          <div class="m-info m-info-basic">
            <div class="m-title">Thông tin cơ bản</div>

            <div class="m-info-item">
              <div class="m-info-name">
                Mã vai trò <span style="color: red">*</span>
              </div>
              <div class="m-info-content">
                <input class="m-input" type="text" v-model="role.RoleCode" />
              </div>
            </div>

            <div class="m-info-item">
              <div class="m-info-name">
                Tên vai trò <span style="color: red">*</span>
              </div>
              <div class="m-info-content">
                <input class="m-input" type="text" v-model="role.RoleName" />
              </div>
            </div>
          </div>

          <div class="m-info m-info-basic" style="width: 800px">
            <div class="m-title" style="padding-bottom: 10px">
              Cho phép quyền
            </div>
            <br />
            <h3 style="color: #757575">Hệ thống</h3>
            <div class="permission-general">
              <div
                class="m-info-item"
                style="display: flex; align-items: unset"
              >
                <label class="m-checkbox-input">
                  Báo cáo
                  <input type="checkbox" v-model="permission.report" />
                  <span class="m-checkbox-checked"></span>
                </label>
              </div>
              <div
                class="m-info-item"
                style="display: flex; align-items: unset; margin-left: 200px"
              >
                <label class="m-checkbox-input">
                  Thiết lập
                  <input type="checkbox" v-model="permission.setting" />
                  <span class="m-checkbox-checked"></span>
                </label>
              </div>
            </div>

            <h3 style="color: #757575">Phân hệ</h3>

            <div class="permission-entity" style="margin-top: 0px">
              <div class="permission-entity-title">
                <div class="m-info-item">
                  <label class="m-checkbox-input"> Đơn vị tính </label>
                </div>
              </div>
              <br />
              <div class="permission-entity-item">
                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Xem
                    <input
                      type="checkbox"
                      v-model="permission.calculationunit_view"
                    />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Thêm
                    <input
                      type="checkbox"
                      v-model="permission.calculationunit_add"
                    />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Sửa
                    <input
                      type="checkbox"
                      v-model="permission.calculationunit_edit"
                    />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Xóa
                    <input
                      type="checkbox"
                      v-model="permission.calculationunit_delete"
                    />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>
              </div>
            </div>

            <div class="permission-entity">
              <div class="permission-entity-title">
                <div class="m-info-item">
                  <label class="m-checkbox-input"> Loại hàng hóa </label>
                </div>
              </div>
              <br />
              <div class="permission-entity-item">
                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Xem
                    <input
                      type="checkbox"
                      v-model="permission.productcategory_view"
                    />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Thêm
                    <input
                      type="checkbox"
                      v-model="permission.productcategory_add"
                    />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Sửa
                    <input
                      type="checkbox"
                      v-model="permission.productcategory_edit"
                    />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Xóa
                    <input
                      type="checkbox"
                      v-model="permission.productcategory_delete"
                    />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>
              </div>
            </div>

            <div class="permission-entity">
              <div class="permission-entity-title">
                <div class="m-info-item">
                  <label class="m-checkbox-input"> Hàng hóa </label>
                </div>
              </div>
              <br />
              <div class="permission-entity-item">
                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Xem
                    <input type="checkbox" v-model="permission.product_view" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Thêm
                    <input type="checkbox" v-model="permission.product_add" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Sửa
                    <input type="checkbox" v-model="permission.product_edit" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Xóa
                    <input
                      type="checkbox"
                      v-model="permission.product_delete"
                    />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>
              </div>
            </div>

            <div class="permission-entity">
              <div class="permission-entity-title">
                <div class="m-info-item">
                  <label class="m-checkbox-input"> Đơn hàng </label>
                </div>
              </div>
              <br />
              <div class="permission-entity-item">
                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Xem
                    <input type="checkbox" v-model="permission.order_view" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Thêm
                    <input type="checkbox" v-model="permission.order_add" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Sửa
                    <input type="checkbox" v-model="permission.order_edit" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Xóa
                    <input type="checkbox" v-model="permission.order_delete" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>
              </div>
            </div>

            <div class="permission-entity">
              <div class="permission-entity-title">
                <div class="m-info-item">
                  <label class="m-checkbox-input"> Nhân viên </label>
                </div>
              </div>
              <br />
              <div class="permission-entity-item">
                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Xem
                    <input type="checkbox" v-model="permission.user_view" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Thêm
                    <input type="checkbox" v-model="permission.user_add" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Sửa
                    <input type="checkbox" v-model="permission.user_edit" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Xóa
                    <input type="checkbox" v-model="permission.user_delete" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>
              </div>
            </div>

            <div class="permission-entity">
              <div class="permission-entity-title">
                <div class="m-info-item">
                  <label class="m-checkbox-input"> Vai trò </label>
                </div>
              </div>
              <br />
              <div class="permission-entity-item">
                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Xem
                    <input type="checkbox" v-model="permission.role_view" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Thêm
                    <input type="checkbox" v-model="permission.role_add" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Sửa
                    <input type="checkbox" v-model="permission.role_edit" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>

                <div class="m-info-item">
                  <label class="m-checkbox-input">
                    Xóa
                    <input type="checkbox" v-model="permission.role_delete" />
                    <span class="m-checkbox-checked"></span>
                  </label>
                </div>
              </div>
            </div>
            <h3 style="color: #757575">Giao hàng</h3>
            <div class="permission-general">
              <div
                class="m-info-item"
                style="display: flex; align-items: unset; height: 2px"
              >
                <label class="m-checkbox-input">
                  Shipper
                  <input type="checkbox" v-model="permission.shipper" />
                  <span class="m-checkbox-checked"></span>
                </label>
              </div>
            </div>
          </div>
        </form>
      </div>

      <div
        class="m-content-action m-detail-action"
        style="border-bottom: 1px solid #ddd"
      >
        <button class="m-btn m-btn-save" @click="onClickSubmit()">
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
import baseRequest from "../../apis/baseRequest";
// import Enum from "@/commons/enum";
// import Resource from "@/commons/resource";

export default {
  components: {},
  props: ["isShowDetail", "formMode", "role", "roleId", "apiRouter"],

  data() {
    return {
      // chế độ submit (true: submit, false: không submit)
      submitted: false,

      // #region permmission
      allPermission: 0,

      permission: {
        shipper: null,
        // hệ thống
        report: null,
        setting: null,

        // phân hệ đơn vị tính
        calculationunit_view: null,
        calculationunit_add: null,
        calculationunit_edit: null,
        calculationunit_delete: null,

        // phân hệ loại hàng hóa
        productcategory_view: null,
        productcategory_add: null,
        productcategory_edit: null,
        productcategory_delete: null,

        // phân hệ hàng hóa
        product_view: null,
        product_add: null,
        product_edit: null,
        product_delete: null,

        // phân hệ đơn hàng
        order_view: null,
        order_add: null,
        order_edit: null,
        order_delete: null,

        // phân hệ user
        user_view: null,
        user_add: null,
        user_edit: null,
        user_delete: null,

        // phân hệ role
        role_view: null,
        role_add: null,
        role_edit: null,
        role_delete: null,
      },
      // #endregion
    };
  },
  watch: {},
  created() {},
  // kiểm tra dữ liệu
  validations: {
    role: {
      roleName: {
        required,
      },
    },
  },
  methods: {
    /**
     * ẩn form
     * createdBy: namnguyen(19/01/2022)
     */
    hideFormData() {
      try {
        this.submitted = false;
        this.$emit("hideRoleDetail");
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
        //debugger; // eslint-disable-line
        let me = this;
        me.submitted = true;
        let arrPermissionTemp = Object.entries(me.permission).filter(
          (item) => item[1] === true
        );
        let rolePermission = arrPermissionTemp
          .map((item) => item[0])
          .toString();
        me.role.LstPermission = rolePermission;
        // validate dữ liệu
        if (this.formMode == Const.FormMode.Add) {
          // sinh guid
          me.role.RoleId = me.uuidv4();
          BaseRequest.post(me.apiRouter, me.role)
            .then(() => {
              this.$emit("GetAllRole");
              this.submitted = false;
              if (me.role.LstPermission) {
                let arrPer = rolePermission.split(",");
                baseRequest
                  .post("Permissions" + "/" + me.role.RoleId, arrPer)
                  .then(function (res) {
                    console.log(res);
                  })
                  .catch(function (e) {
                    console.log(e);
                  });
              }
              this.hideFormData();
            })
            .catch(function (res) {
              console.log(res.response);
            });
        } else {
          // update data
          BaseRequest.put(me.apiRouter + "/" + me.roleId, me.role)
            .then(() => {
              this.$emit("GetAllRole");
              this.submitted = false;

              this.hideFormData();
            })
            .catch(function (res) {
              console.log(res.response.data);
            });
        }
      } catch (error) {
        console.log(error);
      }
    },

    uuidv4() {
      return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, (c) =>
        (
          c ^
          (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
        ).toString(16)
      );
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
};
</script>

<style lang="css" scoped>
.permission-general {
  display: flex;
  height: 20px;
  margin-left: 20px;
}

.permission-entity {
  display: flex;
  width: 800px;
  flex-direction: column;
  margin-left: 20px;
  border-bottom: 1px solid #ccc;
  margin-top: 10px;
}

.permission-entity-title {
  width: 100%;
  height: 20px;
  font-weight: 900;
}

.permission-entity-item {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  margin-left: 30px;
}
</style>
