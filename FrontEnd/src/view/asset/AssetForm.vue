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
              <div class="form--row">
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
              </div>
              <div class="form--row">
                <div class="input__wrapper">
                  <m-combobox
                    ref="DepartmentCode"
                    iconPrefix="icon__arrow--xl"
                    placeholder="Chọn mã bộ phận sử dụng"
                    iconPos="right"
                    name="DepartmentCode"
                    :isFilter="true"
                    :required="true"
                    :listOptions="listDepartment"
                    v-model="asset.DepartmentCode"
                    isLabel="Mã bộ phận sử dụng"
                    @change="changeDepartment"
                  ></m-combobox>
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
              </div>
              <div class="form--row">
                <div class="input__wrapper">
                  <m-combobox
                    ref="CategoryCode"
                    iconPrefix="icon__arrow--xl"
                    placeholder="Chọn mã loại tài sản"
                    iconPos="right"
                    name="CategoryCode"
                    :listOptions="listAssetCategory"
                    :isFilter="true"
                    :required="true"
                    v-model="asset.CategoryCode"
                    isLabel="Mã loại tài sản"
                    @change="changeType"
                  ></m-combobox>
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
              </div>
              <div class="form--row">
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
              <div class="form--row">
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
                    ref="DepreciationYear"
                    type="text"
                    v-model="asset.DepreciationYear"
                    className="input__text input--default text--right"
                    name="DepreciationYear"
                    :required="true"
                    :isLabel="true"
                    :maxLength="18"
                    @input="
                      asset.DepreciationYear = this.formatInputMoney(
                        asset.DepreciationYear
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
              <div class="form--row">
                <div class="input__wrapper">
                  <m-input
                    ref="PurchaseDate"
                    :required="true"
                    :isLabel="true"
                    className="input__text input--default"
                    type="date"
                    name="PurchaseDate"
                    v-model="asset.PurchaseDate"
                  ></m-input>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="ProductionYear"
                    type="date"
                    className="input__text input--default"
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
            <m-button
              @click="handleCancel"
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
  name: "AssetForm",
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
        AssetId: "",
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
        DepreciationYear: "",
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
      if (this.formMode === Enum.FORM__MODE.DUPLICATE) {
        this.getNewAssetCode();
        this.asset.AssetCode = this.newAssetCode;
      }
      this.asset.DepreciationRate = format.formatFloat(
        this.asset.DepreciationRate * 100
      );
    } else {
      this.getNewAssetCode();
      this.asset = {};
      this.asset.AssetCode = this.newAssetCode;
      this.asset.Quantity = 1;
      this.asset.Cost = 0;
      this.asset.DepreciationYear = 0;
    }
    this.isShowForm = this.isShow;
  },

  computed: {
    ...mapState("toastMessage", ["isShowToast", "contentToast"]),
    ...mapState("formDialog", ["formMode"]),
    ...mapState("yearSelected", ["yearSelected"]),

    /**
     * Function set yearSelected
     * Author: HMDUC (20/5/2023)
     */
    setYearCurrent() {
      return this.yearSelected;
    },

    /**
     * Function set title form by formMode
     * Author: HMDUC (20/5/2023)
     */
    titleForm() {
      if (
        this.formMode === Enum.FORM__MODE.ADD ||
        this.formMode === Enum.FORM__MODE.DUPLICATE
      ) {
        return this.$_MISAResources.form__title["AddTitle"];
      } else {
        return this.$_MISAResources.form__title["EditTitle"];
      }
    },

    /**
     * Function check valid Input
     * Author: HMDUC (20/5/2023)
     */
    isValid() {
      for (const inputRef in this.$refs) {
        if (this.$refs[inputRef].required && this.$refs[inputRef].validate()) {
          return false;
        }
      }
      return true;
    },
  },

  mounted() {
    if (this.isShowForm) {
      this.$nextTick(() => {
        this.$refs.AssetCode.focus();
      });
    }
  },

  methods: {
    ...mapActions("formDialog", ["setIsShow"]),
    ...mapActions("property", ["postProperty", "updateProperty"]),

    /**
     * Function call Api Get NewAssetCode
     * Author: HMDUC(15/06/2023)
     */
    async getNewAssetCode() {
      try {
        const res = await request.get(`/Assets/NewCode`);
        this.newAssetCode = res;
        return this.newAssetCode;
      } catch (err) {
        this.$emit(
          "showToast",
          "notice",
          this.$_MISAResources.toast__content.ErrorServer
        );
      }
    },

    /**
     * Function call Api get Department list
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
        this.$emit(
          "showToast",
          "notice",
          this.$_MISAResources.toast__content.ErrorServer
        );
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
        this.$emit(
          "showToast",
          "notice",
          this.$_MISAResources.toast__content.ErrorServer
        );
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
      this.asset.DepreciationRate = format.formatFloat(
        ((1 / this.asset.LifeTime) * 100).toFixed(2)
      );

      //Calculate DepreciationYear when change category
      var DepreciationYear =
        parseFloat(this.asset.Cost).toFixed(3) *
        parseFloat(this.asset.DepreciationRate.replace(",", "."));

      this.asset.DepreciationYear = format.formatMoney(
        Math.round(DepreciationYear)
      );
    },

    /**
     * function find option selected by id when select departmentID or  categoryID
     * Author: HMDUC (01/06/2023)
     * @param {*} arr
     * @param {*} opt
     * @return Id: BP001 -> Phòng kỹ thuật || TS001 -> Máy tính xách tay
     */
    findOptionById(arr, opt) {
      return arr.find((item) => {
        return item.value == opt;
      });
    },

    /**
     * function format money
     * Author: HMDUC (04/06/2023)
     * @param {*} value
     * return 1.000.000
     */
    formatInputMoney(value) {
      //remove character not numbe
      let money = this.checkNumber(value);

      if (this.asset.DepreciationRate && money) {
        var DepreciationYear =
          (parseFloat(money).toFixed(3) *
            parseFloat(this.asset.DepreciationRate?.replace(",", "."))) /
          100;
        this.asset.DepreciationYear = format.formatMoney(
          Math.round(DepreciationYear)
        );
      }

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
      this.asset.Quantity = parseInt(value) + number;
    },

    /**
     * function handle decrease field number
     * Author: HMDUC (03/06/2023)
     * @param {*} value
     * @return value - 1
     */
    handleDecrease(value, number) {
      if (value > 1) {
        this.asset.Quantity = parseInt(value) - number;
      }
    },

    /**
     * function close form
     * Author: HMDUC (28/05/2023)
     */
    closeForm() {
      this.setIsShow(false);
    },

    /**
     * functin clone Asset
     * Author: HMDUC (28/05/2023)
     */
    cloneAsset() {
      var cloneAsset = {
        AssetCode: this.asset.AssetCode,
        AssetName: this.asset.AssetName,
        DepartmentId: this.asset.DepartmentId,
        DepreciationRate:
          parseFloat(this.asset.DepreciationRate?.replace(",", ".")) / 100,
        DepreciationYear: this.asset.DepreciationYear.replace(".", ""),
        Cost: this.asset.Cost?.replace(".", ""),
        CategoryId: this.asset.CategoryId,
        PurchaseDate: this.asset.PurchaseDate,
        Quantity: this.asset.Quantity,
        LifeTime: this.asset.LifeTime,
        ProductionYear: this.asset.ProductionYear,
        TrackedYear: this.yearSelected,
      };
      return cloneAsset;
    },

    /**
     * Function handle btn Cancel form
     * Author: HMDUC (28/05/2023)
     */
    handleCancel() {
      if (this.formMode === Enum.FORM__MODE.EDIT) {
        var assetChanged = {
          ...this.asset,
          DepreciationRate:
            parseFloat(this.asset.DepreciationRate.replace(",", ".")) / 100,
        };
        if (this.assetJson !== JSON.stringify(assetChanged)) {
          this.$emit("openPopup", "warning", {
            ...this.cloneAsset(),
            AssetId: this.asset.AssetId,
            DepartmentCode: this.asset.DepartmentCode,
            CategoryCode: this.asset.CategoryCode,
            DepartmentName: this.asset.DepartmentName,
            CategoryName: this.asset.CategoryName,
            Cost: this.asset.Cost,
            DepreciationYear: this.asset.DepreciationYear,
          });
        } else {
          this.closeForm();
        }
      } else {
        this.$emit("openPopup", "warning", this.asset);
      }

      //check Asset isChanged
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
            //Add Asset
            this.addAsset(this.cloneAsset());
            break;
          case Enum.FORM__MODE.EDIT:
            //Edit Asset

            var assetChanged = {
              ...this.asset,
              DepreciationRate:
                parseFloat(this.asset.DepreciationRate.replace(",", ".")) / 100,
            };

            //check Asset isChanged
            if (this.assetJson !== JSON.stringify(assetChanged)) {
              this.updateAsset({
                ...this.cloneAsset(),
                AssetId: this.asset.AssetId,
              });
            } else {
              //toast
              console.log("f");
            }
            break;
          case Enum.FORM__MODE.DUPLICATE:
            //Duplicate Asset
            this.addAsset(this.cloneAsset());
            break;
          default:
            break;
        }
      }
    },

    /**
     * Function call Api Add Asset
     * Author: HMDUC (28/05/2023)
     * @param {*} asset
     */
    async addAsset(asset) {
      try {
        const res = await this.postProperty(asset);
        if (res.status === Enum.REQ__CODE.CREATED) {
          this.$emit("showToast", "success__add");
          this.closeForm();
        } else if (res.status === Enum.REQ__CODE.BAD_REQUEST) {
          this.$emit(
            "showToast",
            "notice",
            res.data.DataError[asset.AssetCode]
          );
        }
      } catch (err) {
        this.$emit("showToast", "err__add");
      }
    },

    /**
     * Function call Api update Asset
     * Author: HMDUC (28/05/2023)
     * @param {*} asset
     */
    async updateAsset(asset) {
      try {
        const res = await this.updateProperty(asset);
        if (res.status === Enum.REQ__CODE.SUCCESS) {
          this.$emit("showToast", "success__update");
          this.closeForm();
        } else if (res.status === Enum.REQ__CODE.BAD_REQUEST) {
          this.$emit(
            "showToast",
            "notice",
            res.data.DataError[asset.AssetCode]
          );
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
