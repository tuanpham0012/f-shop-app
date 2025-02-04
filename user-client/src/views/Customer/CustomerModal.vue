<template>
  <modal
    :title="!id ? 'Thêm mới khách hàng' : 'Cập nhật khách hàng'"
    modal-size="modal-lg"
    @close="
      () => {
        emits('close');
      }
    "
  >
    <template #body>
      <div class="col-sm-12">
        <div class="row">
          <div class="col-sm-6 mb-3">
            <div class="col-sm-12">
              <label for="name" class="form-label required">Họ tên</label>
            </div>
            <div class="input-group input-group-merge">
              <span id="basic-icon-default-fullname2" class="input-group-text"
                ><i class="bx bx-user"></i
              ></span>
              <input
                type="text"
                class="form-control"
                id="name"
                v-model="customer.name"
              />
            </div>
            <Feedback :errors="errors?.Name" />
          </div>

          <div class="col-sm-6 mb-3">
            <div class="col-sm-12">
              <label for="email" class="form-label required">Email</label>
            </div>
            <div class="input-group input-group-merge">
              <span class="input-group-text"
                ><i class="bx bx-envelope"></i
              ></span>
              <input
                type="email"
                class="form-control"
                id="email"
                v-model="customer.email"
              />
            </div>
            <Feedback :errors="errors?.Email" />
          </div>
          <div class="col-sm-6 mb-3">
            <div class="col-sm-12">
              <label for="phone" class="form-label">Điện thoại</label>
            </div>
            <div class="input-group input-group-merge">
              <span id="basic-icon-default-phone2" class="input-group-text"
                ><i class="bx bx-phone"></i
              ></span>
              <input
                type="text"
                class="form-control"
                v-model="customer.phone"
              />
            </div>
            <Feedback :errors="errors?.Phone" />
          </div>

          <div class="col-sm-6 mb-3" v-if="!props.id">
            <div class="col-sm-12">
              <label for="password" class="form-label required">Password</label>
            </div>
            <div class="input-group input-group-merge">
              <span id="basic-icon-default-password" class="input-group-text"
                ><i class="bx bx-lock"></i
              ></span>
              <input
                :type="showPassword ? 'text' : 'password'"
                class="form-control"
                v-model="customer.password"
              />
            </div>
            <div class="form-check mt-1">
              <input
                class="form-check-input"
                type="checkbox"
                v-model="showPassword"
                id="defaultCheck1"
              />
              <label class="form-check-label" for="defaultCheck1">
                Hiện mật khẩu
              </label>
            </div>
            <Feedback :errors="errors?.Password" />
          </div>
          <div class="col-sm-6 mb-3">
            <div class="col-sm-12">
              <label for="gender" class="form-label">Giới tính</label>
            </div>
            <select class="form-select" v-model="customer.gender" id="gender">
              <option selected :value="0">Khác</option>
              <option selected :value="1">Nam</option>
              <option selected :value="2">Nữ</option>
            </select>
            <Feedback :errors="errors?.gender" />
          </div>
          <div class="col-sm-6 mb-3">
            <div class="col-sm-12">
              <label for="gender" class="form-label">Trạng thái</label>
            </div>
            <select class="form-select" v-model="customer.status" id="gender">
              <option selected :value="0">Chưa kích hoạt</option>
              <option selected :value="1">Hoạt động</option>
              <option selected :value="2">Đã khoá</option>
            </select>
            <Feedback :errors="errors?.Status" />
          </div>
          
          <div class="col-sm-12 mb-3">
            <div class="col-sm-2">
              <label for="note" class="form-label">Địa chỉ</label>
            </div>
            <div class="input-group input-group-merge">
              <span id="basic-icon-default-address" class="input-group-text"
                ><i class="bx bxs-map"></i
              ></span>
              <input
                type="text"
                class="form-control"
                id="address"
                placeholder="Địa chỉ chi tiết"
                v-model="customer.address"
              />
            </div>
            <Feedback :errors="errors?.Address" />
          </div>
        </div>
      </div>
    </template>
    <template #footer>
      <button class="btn btn-success" @click="save()">Lưu lại</button>
      <button
        class="btn btn-secondary"
        @click="
          () => {
            emits('close');
          }
        "
      >
        Đóng
      </button>
    </template>
  </modal>
</template>
<script lang="ts" setup>
import { ref, reactive, computed, onBeforeMount } from "vue";
import { useCustomerStore } from "@/stores/customer";
import { successMessage } from "@/helpers/toast";
const props = defineProps({
  id: {
    type: [Number, String as () => string | null],
    required: false,
    default: null,
  },
});

const emits = defineEmits(["close"]);

const customerStore = useCustomerStore();

const newCustomer = reactive({
  id: null,
  name: "",
  email: "",
  phone: "",
  address: "",
  password: "",
  status: 0,
  gender: 0
});

const customer = computed(() =>
  props.id && customerStore.$state.customer
    ? customerStore.$state.customer
    : newCustomer
);

const errors = ref<any>(null);

const showPassword = ref<Boolean>(false)

const save = async () => {
  if (customer.value.id == null) {
    await customerStore
      .create(customer.value)
      .then((res) => {
        console.log(res);
        successMessage(res.data?.message ?? "Thêm mới thành công!");
        emits("close", true);
      })
      .catch((err) => {
        console.log(err);
        errors.value = err.response.data.errors;
      });
  } else {
    await customerStore
      .update(props.id, customer.value)
      .then((res) => {
        successMessage(res.data?.message ?? "Cập nhật thành công!");
        emits("close", true);
      })
      .catch((err) => {
        console.log(err);
        errors.value = err.response.data.errors;
      });
  }
};
onBeforeMount(() => {
  if (props.id) {
    customerStore.show(props.id);
  }
});
</script>
<style lang=""></style>
