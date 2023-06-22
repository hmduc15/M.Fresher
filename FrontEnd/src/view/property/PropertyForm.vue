<template>
  <m-dialog v-if="isShowForm">
    <template #content>
      <div class="form__wrapper">
        <form class="form__container" v-esc="closeForm">
          <div class="form__body">
            <m-button
              type="button"
              className="btn__close"
              iconButton="icon__close"
              @click="closeForm"
              title="Đóng"
              posTooltip="right"
            ></m-button>
            <p class="form__title">{{ titleForm }}</p>
            <div class="form__input--container">
              <div class="form--left">
                <div class="input__wrapper">
                  <m-input
                    ref="AssetCode"
                    type="text"
                    className="input__text input--default"
                    name="AssetCode"
                    :required="true"
                    :isLabel="true"
                    :isFocus="true"
                    :maxLength="20"
                    v-model="asset.AssetCode"
                  >
                  </m-input>
                </div>
                <div class="input__wrapper">
                  <m-combobox
                    ref="DepartmentCode"
                    icon="icon__arrow--xl"
                    placeholder="Chọn mã bộ phận sử dụng"
                    iconPos="right"
                    name="DepartmentCode"
                    :isFilter="true"
                    :listOptions="listDepartment"
                    v-model="asset.DepartmentCode"
                    isLabel="Mã bộ phận sử dụng"
                    @change="changeDepartment"
                  ></m-combobox>
                </div>
                <div class="input__wrapper">
                  <m-combobox
                    ref="department_name"
                    icon="icon__arrow--xl"
                    placeholder="Chọn mã loại tài sản"
                    iconPos="right"
                    name="CategoryCode"
                    :listOptions="listAssetCategory"
                    :isFilter="true"
                    v-model="asset.CategoryCode"
                    isLabel="Mã loại tài sản"
                    @change="changeType"
                  ></m-combobox>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="Quantity"
                    type="text"
                    v-model="asset.Quantity"
                    className="input__text input--default text--right"
                    name="Quantity"
                    :number="true"
                    :required="true"
                    :isLabel="true"
                    :maxLength="18"
                    @input="asset.Quantity = this.checkNumber(asset.Quantity)"
                  >
                  </m-input>
                  <div class="input__btn">
                    <m-button
                      type="button"
                      iconButton="icon__up"
                      @click="handleIncrease(this.asset.Quantity, 1)"
                    ></m-button>
                    <m-button
                      type="button"
                      iconButton="icon__down"
                      @click="handleDecrease(this.asset.Quantity, 1)"
                    ></m-button>
                  </div>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="DepreciationRate"
                    type="text"
                    v-model="asset.DepreciationRate"
                    className="input__text input--default text--right"
                    name="DepreciationRate"
                    :number="true"
                    :required="true"
                    :isLabel="true"
                    :maxLength="18"
                    @input="
                      asset.DepreciationRate = this.checkNumber(
                        asset.DepreciationRate
                      )
                    "
                  >
                  </m-input>
                  <div class="input__btn">
                    <m-button type="button" iconButton="icon__up"></m-button>
                    <m-button type="button" iconButton="icon__down"></m-button>
                  </div>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="PurchaseDate"
                    :required="true"
                    :isLabel="true"
                    label="Ngày mua"
                    type="date"
                    name="PurchaseDate"
                    className=" input--default text--left"
                    v-model="asset.PurchaseDate"
                  ></m-input>
                </div>
              </div>
              <div class="form--right">
                <div class="input__wrapper">
                  <m-input
                    ref="AssetName"
                    type="text"
                    v-model="asset.AssetName"
                    className="input__text input--xl"
                    name="AssetName"
                    :required="true"
                    :maxLength="36"
                    :isLabel="true"
                  >
                  </m-input>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="DepartmentName"
                    type="text"
                    v-model="asset.DepartmentName"
                    className="input__text input--xl"
                    name="DepartmentName"
                    :required="false"
                    :isLabel="true"
                    :isDisabled="true"
                  >
                  </m-input>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="CategoryName"
                    type="text"
                    v-model="asset.CategoryName"
                    className="input__text input--xl"
                    name="CategoryName"
                    :required="false"
                    :isLabel="true"
                    :isDisabled="true"
                  >
                  </m-input>
                </div>
                <div class="input__row">
                  <div class="input__wrapper">
                    <m-input
                      ref="Cost"
                      type="text"
                      v-model="asset.Cost"
                      className="input__text input--default text--right"
                      name="Cost"
                      :required="true"
                      :maxLength="18"
                      :isLabel="true"
                      @input="asset.Cost = this.formatInputMoney(asset.Cost)"
                    >
                    </m-input>
                  </div>
                  <div class="input__wrapper">
                    <m-input
                      ref="LifeTime"
                      type="text"
                      value=""
                      className="input__text input--default text--right"
                      name="LifeTime"
                      :required="true"
                      :isLabel="true"
                      v-model="asset.LifeTime"
                    >
                    </m-input>
                  </div>
                </div>
                <div class="input__row">
                  <div class="input__wrapper">
                    <m-input
                      ref="CostDepreciation"
                      type="text"
                      v-model="asset.CostDepreciation"
                      className="input__text input--default text--right"
                      name="CostDepreciation"
                      :required="true"
                      :isLabel="true"
                      :maxLength="18"
                      @input="
                        asset.CostDepreciation = this.formatInputMoney(
                          asset.CostDepreciation
                        )
                      "
                    >
                    </m-input>
                  </div>
                  <div class="input__wrapper">
                    <m-input
                      ref="TrackedYear"
                      type="text"
                      value=""
                      className="input__text input--default text--right"
                      name="TrackedYear"
                      :required="true"
                      :isLabel="true"
                      :isDisabled="true"
                      v-model="this.setYearCurrent"
                    >
                    </m-input>
                  </div>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="ProductionYear"
                    type="date"
                    className=" input--default text--left"
                    v-model="asset.ProductionYear"
                    name="ProductionYear"
                    :required="true"
                    :isLabel="true"
                  ></m-input>
                </div>
              </div>
            </div>
          </div>
          <div class="form__footer">
            <div class="form__button">
              <m-button
                @click="closeForm"
                type="button"
                content="Hủy bỏ"
                className="btn__sub btn--form "
              ></m-button>
              <m-button
                type="button"
                content="Lưu"
                className="btn__main btn__submit"
                @click="submitData"
              ></m-button>
            </div>
          </div>
        </form>
      </div>
    </template>
  </m-dialog>
</template>
<script>
import { mapActions, mapState } from "vuex";
import Enum from "@/utils/enum";
import { format } from "@/utils/format";
import { request } from "@/services/request";

import MDiaglog from "@/components/base/MDiaglog.vue";
import MInput from "@/components/base/MInput.vue";
import MButton from "@/components/base/MButton.vue";
import MCombobox from "@/components/base/MCombobox";

export default {
  name: "PropertyForm",
  props: ["title", "isShow", "data"],
  components: {
    "m-dialog": MDiaglog,
    "m-input": MInput,
    "m-button": MButton,
    "m-combobox": MCombobox,
  },
  data() {
    return {
      dataRow: this.data,
      isShowForm: this.isShow,
      asset: {
        AssetCode: "",
        AssetName: "",
        DepartmentId: "",
        DepartmentCode: "",
        DepartmentName: "",
        CategoryId: "",
        CategoryCode: "",
        CategoryName: "",
        PurchaseDate: "",
        Cost: "",
        Quantity: "",
        DepreciationRate: "",
        TrackedYear: "",
        LifeTime: "",
        ProductionYear: "",
        CreatedBy: "",
        CreatedDate: "",
        ModifiedBy: "",
        ModifiedDate: "",
      },
      assetJson: null,
      isError: false,
      listDepartment: null,
      listAssetCategory: null,
      selectedOption: null,
      newAssetCode: null,
    };
  },

  created() {
    this.getDepartmentList();
    this.getCategory();
    this.getNewAssetCode();
  },

  updated() {
    if (this.data) {
      let objJson = JSON.stringify(this.data);
      this.assetJson = objJson;
      this.asset = JSON.parse(objJson);
    } else {
      this.asset = {};
      this.asset.AssetCode = this.newAssetCode;
      this.getNewAssetCode();
    }
    this.isShowForm = this.isShow;
  },

  computed: {
    ...mapState("toastMessage", ["isShowToast", "contentToast"]),
    ...mapState("formDialog", ["formMode"]),
    ...mapState("yearSelected", ["yearSelected"]),

    setYearCurrent() {
      return this.yearSelected;
    },

    titleForm() {
      return this.data
        ? this.$_MISAResources.form__title["EditTitle"]
        : this.$_MISAResources.form__title["AddTitle"];
    },

    isValid() {
      for (const inputRef in this.$refs) {
        if (this.$refs[inputRef].required && this.$refs[inputRef].validate()) {
          return false;
        }
      }
      return true;
    },
  },

  methods: {
    ...mapActions("formDialog", ["setIsShow"]),
    ...mapActions("property", ["postProperty", "updateProperty"]),

    async getNewAssetCode() {
      try {
        const res = await request.get(`/Assets/NewCode`);
        let [prefix, suffix] = res.split("-");
        this.newAssetCode = `${prefix}-` + `${parseInt(suffix) + 1}`;
        return this.newAssetCode;
      } catch (err) {
        console.log(err);
      }
    },

    /**
     * Function call Api get Departmen list
     * Author: HMDUC (15/06/2023)
     */
    async getDepartmentList() {
      try {
        const res = await request.get(`/Departments`);
        const mapData = res.map((item) => ({
          id: item.DepartmentId,
          label: item.DepartmentCode,
          value: item.DepartmentName,
        }));
        this.listDepartment = mapData;
      } catch (err) {
        console.log(err);
      }
    },

    /**
     * Function call Api Category list
     * Author: HMDUC (15/06/2023)
     */
    async getCategory() {
      try {
        const res = await request.get(`/Category`);
        const mapData = res.map((item) => ({
          id: item.CategoryId,
          label: item.CategoryCode,
          value: item.CategoryName,
          lifeTime: item.LifeTime,
          depreciationRate: item.DepreciationRate,
        }));
        this.listAssetCategory = mapData;
      } catch (err) {
        console.log(err);
      }
    },

    /**
     * function handle select option
     * Author: HMDUC (01/06/2023)
     * @param {*} obj contain id and name department
     *
     */
    changeDepartment(obj) {
      this.selectedOption = obj;
      const department = this.findOptionById(
        this.listDepartment,
        this.selectedOption
      );
      this.asset.DepartmentId = department.id;
      this.asset.DepartmentName = department.value;
    },
    /**
     * function handle select option
     * Author: HMDUC (01/06/2023)
     * @param {*} obj
     *
     */
    changeType(obj) {
      this.selectedOption = obj;
      const category = this.findOptionById(
        this.listAssetCategory,
        this.selectedOption
      );
      this.asset.CategoryId = category.id;
      this.asset.CategoryName = category.value;
      this.asset.LifeTime = category.lifeTime;
    },
    /**
     * function find option selected by id when select departmentID or propertyID
     * Author: HMDUC (01/06/2023)
     * @param {*} arr
     * @param {*} opt
     * @return Id: BP001 -> Phòng kỹ thuật || TS001 -> Máy tính xách tay
     */
    findOptionById(arr, opt) {
      return arr.find((item) => {
        return item.label == opt;
      });
    },
    /**
     * function format money
     * Author: HMDUC (04/06/2023)
     * @param {*} value
     * return 1.000.000
     */
    formatInputMoney(value) {
      //remove character not number
      let money = this.checkNumber(value);

      //check invalid money input
      if (/^\d+$/.test(money)) {
        let formatted = format.formatMoney(money);
        return formatted;
      }
    },
    /**
     * function require value input is number
     * Author: HMDUC (03/06/2023)
     * @param {*} value
     * @return number: 9999
     */
    checkNumber(value) {
      return value.replace(/\D/g, "");
    },
    /**
     * function handle increase field number
     * Author: HMDUC (03/06/2023)
     * @param {*} value
     * @return value + 1
     */
    handleIncrease(value, number) {
      this.property.propertyQuantity = parseInt(value) + number;
    },

    /**
     * function handle decrease field number
     * Author: HMDUC (03/06/2023)
     * @param {*} value
     * @return value - 1
     */
    handleDecrease(value, number) {
      this.property.propertyQuantity = parseInt(value) - number;
    },

    /**
     * function close form
     * Author: HMDUC (28/05/2023)
     */
    closeForm() {
      this.setIsShow(false);
    },

    /**
     * Function unformat data before send server
     * Author: HMDUC (15/06/2023)
     * @return Object clone object Asset
     */
    unFormatAsset() {
      let cloneAsset = {};
      cloneAsset.AssetCode = this.asset.AssetCode;
      cloneAsset.AssetName = this.asset.AssetName;
      cloneAsset.DepartmentId = this.asset.DepartmentId;
      cloneAsset.CategoryId = this.asset.CategoryId;
      cloneAsset.PurchaseDate = this.asset.PurchaseDate;
      cloneAsset.Quantity = this.asset.Quantity;
      cloneAsset.LifeTime = this.asset.LifeTime;
      cloneAsset.ProductionYear = this.asset.ProductionYear;
      cloneAsset.DepreciationRate = this.asset.DepreciationRate;
      cloneAsset.Cost = this.asset.Cost.replace(".", "");
      cloneAsset.TrackedYear = this.yearSelected;
      return cloneAsset;
    },

    /**
     * function submit form
     * Author: HMDUC (28/05/2023)
     */
    submitData() {
      let mode = this.formMode;

      if (this.isValid) {
        switch (mode) {
          case Enum.FORM__MODE.ADD:
            //Add propert
            this.addProperty(this.unFormatAsset());
            break;
          case Enum.FORM__MODE.EDIT:
            //Edit property
            console.log(this.assetJson === JSON.stringify(this.asset));

            break;
          default:
            break;
        }
      }
    },
    async addProperty(asset) {
      try {
        const res = await this.postProperty(asset);
        console.log(res.status);
        if (res.status === Enum.REQ_CODE.CREATED) {
          this.$emit("showToast", "success__add");
          this.closeForm();
        } else if (res.status === Enum.REQ_CODE.BAD_REQUEST) {
          this.$emit("showToast", "notice", res.data.Data[asset.AssetCode]);
        }
      } catch (err) {
        this.$emit("showToast", "err__add");
      }
    },
  },
};
</script>
<style>
@import "@/css/base/form.css";
</style>
