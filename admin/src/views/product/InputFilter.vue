<template>
  <div class="m-filter">
    <div class="m-btn m-btn-context">    
      <v-select
        class="m-modal-col-6"
        :get-option-label="(option) => option.Name"
        :options="operators"
        :reduce="(option) => option.Value"
        v-model="operator"
        @input="sltOperatorOnChange($event)"
      >
        <template #option="{ Show }">
          {{ Show }}
        </template>
      </v-select>
    </div>
    <input
      type="text"
      class="m-input m-input-thead"
      v-model="inputValue"
      @keyup.enter="onChangeInputValue"
    />
  </div>
</template>
<script>
import Enum from "@/commons/enum";
export default {
  props: ["column", "valueType"],
  mounted() {
    if (this.valueType == Enum.ValueType.Int) {
      this.operators = this.operatorInt;
      this.operator = Enum.Operator.LessThan;
    } else if (this.valueType == Enum.ValueType.String) {
      this.operators = this.operatorString;
      this.operator = Enum.Operator.Contain;
    }
  },
  data() {
    return {
      /* Toán tử mặc định là Chứa */
      operator: null,
      operators: [],
      /* Danh sách các toán tử chuỗi*/
      operatorString: [
        { Name: "∗", Show: "* : Chứa", Value: Enum.Operator.Contain },
        { Name: "=", Show: "= : Bằng", Value: Enum.Operator.EqualTo },
        {
          Name: "+",
          Show: "+ : Bắt đầu bằng",
          Value: Enum.Operator.BeginWith,
        },
        { Name: "-", Show: "- : Kết thúc bằng", Value: Enum.Operator.EndWith },
        { Name: "!", Show: "! : Không chứa", Value: Enum.Operator.NotContain },
      ],
      /* Danh sách các toán tử số*/
      operatorInt: [
        { Name: "=", Show: "= : Bằng", Value: Enum.Operator.EqualTo },
        {
          Name: "<",
          Show: "< : Nhỏ hơn",
          Value: Enum.Operator.LessThan,
        },
        {
          Name: "≤",
          Show: "≤ : Nhỏ hơn hoặc bằng",
          Value: Enum.Operator.LessThanOrEqualTo,
        },
        { Name: ">", Show: "> : Lớn hơn", Value: Enum.Operator.MoreThan },
        {
          Name: "≥",
          Show: "≥ : Lớn hơn hoặc bằng",
          Value: Enum.Operator.MoreThanOrEqualTo,
        },
      ],
      /* Giá trị của ô input */
      inputValue: null,
      objFilter: {},
    };
  },
  methods: {

    sltOperatorOnChange() {
      this.onChangeInputValue();
    },

    /**
     * theo dõi sự thay đổi của biến inputValue
     * Nếu inputValue có giá trị nhập thì tạo mới 1 đối tượng lọc
     * createdBy: namnguyen (23/01/2022)
     */
    onChangeInputValue() {
      this.objFilter = {
        Column: this.column,
        Value: this.inputValue,
        Operator: this.operator,
        ValueType: this.valueType,
        AdditionalOperator: Enum.AdditionalOperator.And,
      };
      this.$emit("onChangeInputValue", this.objFilter);
    },
  }
};
</script>

<style lang="css">
</style>
