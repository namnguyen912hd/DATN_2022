<template>
  <div>
    <div class="m-content m-content-detail" v-if="isShowDetail">
      <div class="m-content-action m-detail-action" style="border-bottom: 1px solid #ddd">
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
              <div class="m-info-name">Mã nhân viên</div>
              <div class="m-info-content">
                <input class="m-input" type="text" v-model="user.UserCode" />
              </div>
            </div>

            <div class="m-info-item">
              <div class="m-info-name">Tên nhân viên</div>
              <div class="m-info-content">
                <input class="m-input" type="text" v-model="user.UserName" />
              </div>
            </div>

            <div class="m-info-item">
              <div class="m-info-name">Password</div>
              <div class="m-info-content">
                <input class="m-input" type="password" v-model="user.Password" />
              </div>
            </div>

            <div class="m-info-item">
              <div class="m-info-name">Email</div>
              <div class="m-info-content">
                <input class="m-input" type="text" v-model="user.Email" />
              </div>
            </div>

            <div class="m-info-item">
              <div class="m-info-name">Số điện thoại</div>
              <div class="m-info-content">
                <input class="m-input" type="text" v-model="user.Tel" />
              </div>
            </div>

            <div class="m-info-item">
              <div class="m-info-name">Địa chỉ</div>
              <div class="m-info-content">
                <input class="m-input" type="text" v-model="user.Address" />
              </div>
            </div>

            <div class="m-info-item">
              <div class="m-info-name">Vai trò</div>
              <div class="m-info-content">
                <v-select :options="roles" label="RoleName" :reduce="(option) => option.RoleId"
                  v-model="user.RoleId">
                  <template #list-header> </template>
                  <template v-slot:option="option">
                    {{ option.RoleName }}
                  </template>
                  <template v-slot:no-options="{ search, searching }">
                    <template v-if="searching">
                      Không tìm thấy giá trị <em>{{ search }}</em>.
                    </template>
                    <em v-else style="opacity: 0.5">Điền vào ô để tìm kiếm</em>
                  </template>
                </v-select>
                <div class="m-btn m-btn-quickadd">
                  <div class="m-bg-image"></div>
                </div>
              </div>
            </div>
          </div>
        </form>
      </div>

      <div class="m-content-action m-detail-action" style="border-bottom: 1px solid #ddd">
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
// import Enum from "@/commons/enum";
// import Resource from "@/commons/resource";

export default {
  components: {},
  props: ["isShowDetail", "formMode", "user", "userId", "apiRouter"],

  data() {
    return {

      roles: [],
      // chế độ submit (true: submit, false: không submit)
      submitted: false,
    };
  },
  watch: {
  },
  created() {
    this.getRoles();
    this.user.RoleId = null;
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
    //#region  binding dữ liệu cho các combobox
    /**
     * lấy dữ liệu nhóm hàng hóa
     * createdBy:: namnguyen(24/01/2022)
     */
    getRoles() {
      BaseRequest.get("Roles")
        .then((response) => {
          // gán dữ liệu trả về từ api
          this.roles = response.data;
        })
        .catch((e) => {
          console.log(e);
        });
    },
    //#endregion

    /**
     * ẩn form
     * createdBy: namnguyen(19/01/2022)
     */
    hideFormData() {
      try {
        this.submitted = false;
        this.$emit("hideUserDetail");
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
        // validate dữ liệu
        if (this.formMode == Const.FormMode.Add) {
              BaseRequest.post(me.apiRouter, me.user)
                .then(() => {
                  this.$emit("getAlluser");
                  me.submitted = false;
                  me.hideFormData();
                  
                })
                .catch(function (res) {
                  console.log(res.response);
                });
            } else {
              // update dat
              BaseRequest.put(me.apiRouter + "/" + me.userId, me.user)
                .then(() => {
                  this.$emit("getAlluser");
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
</style>
