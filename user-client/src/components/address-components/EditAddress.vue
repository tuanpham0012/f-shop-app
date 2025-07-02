<template>
    <!-- Overlay -->
    <div class="modal-overlay" @click="closeModal">
        <!-- Address List Modal -->
        <div v-if="currentView === 'list'" class="modal1" @click.stop>
            <div class="modal-header">
                <h2>Địa Chỉ Của Tôi</h2>
                <button @click="closeModal" class="close-btn">×</button>
            </div>

            <div class="modal-body">
                <div class="address-list">
                    <div v-for="address in addresses" :key="address.id" class="address-item">
                        <div class="address-icon">
                            <div class="location-dot"></div>
                        </div>
                        <div class="address-content">
                            <div class="address-name">{{ address.name }}</div>
                            <div class="address-phone">{{ address.phone }}</div>
                            <div class="address-detail">{{ address.detail }}</div>
                            <span v-if="address.isDefault" class="default-badge">Mặc định</span>
                        </div>
                        <button class="update-btn">Cập nhật</button>
                    </div>
                </div>

                <button @click="showAddAddressForm" class="add-address-btn">
                    <span class="plus-icon">+</span>
                    Thêm Địa Chỉ Mới
                </button>
            </div>

            <div class="modal-footer">
                <button @click="closeModal" class="cancel-btn">Hủy</button>
                <button class="confirm-btn">Xác nhận</button>
            </div>
        </div>

        <!-- Add Address Form Modal -->
        <div v-if="currentView === 'add'" class="modal1" @click.stop>
            <div class="modal-header">
                <h2>Địa chỉ mới</h2>
                <button @click="closeModal" class="close-btn">×</button>
            </div>

            <div class="modal-body">
                <form @submit.prevent="submitForm">
                    <div class="form-row">
                        <div class="form-group">
                            <input v-model="newAddress.fullName" type="text" placeholder="Họ và tên" class="form-input" required />
                        </div>
                        <div class="form-group">
                            <input v-model="newAddress.phone" type="tel" placeholder="Số điện thoại" class="form-input" required />
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group">
                            <select v-model="newAddress.location" class="form-select" required>
                                <option value="">Tỉnh/Thành phố</option>
                                <option value="hanoi">Hà Nội</option>
                                <option value="hochiminh">Hồ Chí Minh</option>
                                <option value="danang">Đà Nẵng</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <select v-model="newAddress.location" class="form-select" required>
                                <option value="">Phường/Xã</option>
                                <option value="phuong1">Phường 1</option>
                                <option value="phuong2">Phường 2</option>
                                <option value="phuong3">Phường 3</option>
                            </select>
                        </div>
                    </div>

                    <div class="form-group">
                        <textarea v-model="newAddress.detailAddress" placeholder="Địa chỉ cụ thể" class="form-textarea" rows="3" required></textarea>
                    </div>

                    <button type="button" class="add-location-btn">
                        <span class="plus-icon">+</span>
                        Thêm vị trí
                    </button>

                    <div class="address-type">
                        <span class="address-type-label">Loại địa chỉ:</span>
                        <div class="address-type-options">
                            <label class="address-type-option">
                                <input type="radio" v-model="newAddress.type" value="home" name="addressType" />
                                <span class="radio-label">Nhà Riêng</span>
                            </label>
                            <label class="address-type-option">
                                <input type="radio" v-model="newAddress.type" value="office" name="addressType" />
                                <span class="radio-label">Văn Phòng</span>
                            </label>
                        </div>
                    </div>
                </form>
            </div>

            <div class="modal-footer">
                <button @click="backToList" class="cancel-btn">Trở Lại</button>
                <button @click="submitForm" class="confirm-btn">Hoàn thành</button>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { ref, reactive } from "vue";

interface Address {
    id: number;
    name: string;
    phone: string;
    detail: string;
    isDefault: boolean;
}

interface NewAddress {
    fullName: string;
    phone: string;
    location: string;
    detailAddress: string;
    type: string;
}

const showAddressModal = ref(false);
const currentView = ref<"list" | "add">("list");

const addresses = ref<Address[]>([
    {
        id: 1,
        name: "Phạm Quốc Tuấn",
        phone: "(+84) 983 776 901",
        detail: "Công Ty Cp Fm Quảng Ich, Số 46, Gamuda City\nPhường Yên Sở, Quận Hoàng Mai, Hà Nội",
        isDefault: true,
    },
    {
        id: 2,
        name: "Phạm Quốc Tuấn",
        phone: "(+84) 983 776 901",
        detail: "Số 111, Đường Khương Đình\nPhường Thượng Đình, Quận Thanh Xuân, Hà Nội",
        isDefault: false,
    },
]);

const newAddress = reactive<NewAddress>({
    fullName: "",
    phone: "",
    location: "",
    detailAddress: "",
    type: "home",
});

const showAddAddressForm = () => {
    currentView.value = "add";
};

const backToList = () => {
    currentView.value = "list";
};

const closeModal = () => {
    showAddressModal.value = false;
    currentView.value = "list";
    // Reset form
    Object.assign(newAddress, {
        fullName: "",
        phone: "",
        location: "",
        detailAddress: "",
        type: "home",
    });
};

const submitForm = () => {
    // Add validation logic here
    if (newAddress.fullName && newAddress.phone && newAddress.location && newAddress.detailAddress) {
        // Add new address to list
        addresses.value.push({
            id: addresses.value.length + 1,
            name: newAddress.fullName,
            phone: newAddress.phone,
            detail: `${newAddress.detailAddress}\n${newAddress.location}`,
            isDefault: false,
        });

        // Close modal and reset
        closeModal();
        alert("Địa chỉ đã được thêm thành công!");
    } else {
        alert("Vui lòng điền đầy đủ thông tin!");
    }
};
</script>

<style scoped>
.open-modal-btn:hover {
    background-color: #e55a2b;
}

.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 1000;
}

.modal1 {
    background: white;
    border-radius: 12px;
    width: 100%;
    max-width: 600px;
    max-height: 90vh;
    overflow: hidden;
    display: flex;
    flex-direction: column;
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.3);
}

.modal-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 20px;
    border-bottom: 1px solid #e0e0e0;
}

.modal-header h2 {
    margin: 0;
    font-size: 18px;
    font-weight: 600;
    color: #333;
}

.close-btn {
    background: none;
    border: none;
    font-size: 24px;
    cursor: pointer;
    color: #666;
    width: 30px;
    height: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
}

.close-btn:hover {
    color: #333;
}

.modal-body {
    flex: 1;
    padding: 20px;
    overflow-y: auto;
}

.address-list {
    margin-bottom: 20px;
}

.address-item {
    display: flex;
    align-items: flex-start;
    padding: 16px 0;
    border-bottom: 1px solid #f0f0f0;
}

.address-item:last-child {
    border-bottom: none;
}

.address-icon {
    margin-right: 12px;
    margin-top: 4px;
}

.location-dot {
    width: 12px;
    height: 12px;
    background-color: #ff6b35;
    border-radius: 50%;
}

.address-content {
    flex: 1;
}

.address-name {
    font-weight: 600;
    color: #333;
    margin-bottom: 4px;
}

.address-phone {
    color: #666;
    font-size: 14px;
    margin-bottom: 4px;
}

.address-detail {
    color: #666;
    font-size: 14px;
    line-height: 1.4;
    white-space: pre-line;
}

.default-badge {
    display: inline-block;
    background-color: #ff6b35;
    color: white;
    font-size: 12px;
    padding: 2px 8px;
    border-radius: 4px;
    margin-top: 8px;
}

.update-btn {
    background: none;
    border: 1px solid #4a90e2;
    color: #4a90e2;
    padding: 8px 16px;
    border-radius: 4px;
    font-size: 14px;
    cursor: pointer;
    transition: all 0.2s;
}

.update-btn:hover {
    background-color: #4a90e2;
    color: white;
}

.add-address-btn {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100%;
    padding: 16px;
    border: 2px dashed #ccc;
    background: none;
    color: #666;
    border-radius: 8px;
    cursor: pointer;
    transition: all 0.2s;
}

.add-address-btn:hover {
    border-color: #ff6b35;
    color: #ff6b35;
}

.plus-icon {
    margin-right: 8px;
    font-size: 18px;
    font-weight: bold;
}

.form-row {
    display: flex;
    gap: 16px;
    margin-bottom: 16px;
}

.form-group {
    flex: 1;
    margin-bottom: 16px;
}

.form-input,
.form-select,
.form-textarea {
    width: 100%;
    padding: 12px;
    border: 1px solid #ddd;
    border-radius: 6px;
    font-size: 14px;
    transition: border-color 0.2s;
    box-sizing: border-box;
}

.form-input:focus,
.form-select:focus,
.form-textarea:focus {
    outline: none;
    border-color: #4a90e2;
}

.form-textarea {
    resize: vertical;
    min-height: 80px;
}

.add-location-btn {
    display: flex;
    align-items: center;
    background: none;
    border: none;
    color: #4a90e2;
    cursor: pointer;
    margin-bottom: 20px;
    font-size: 14px;
}

.add-location-btn:hover {
    text-decoration: underline;
}

.address-type {
    margin-bottom: 20px;
}

.address-type-label {
    display: block;
    margin-bottom: 12px;
    font-weight: 500;
    color: #333;
}

.address-type-options {
    display: flex;
    gap: 20px;
}

.address-type-option {
    display: flex;
    align-items: center;
    cursor: pointer;
}

.address-type-option input[type="radio"] {
    margin-right: 8px;
}

.radio-label {
    padding: 8px 16px;
    border: 1px solid #ddd;
    border-radius: 20px;
    font-size: 14px;
    transition: all 0.2s;
}

.address-type-option input[type="radio"]:checked + .radio-label {
    background-color: #4a90e2;
    color: white;
    border-color: #4a90e2;
}

.modal-footer {
    display: flex;
    justify-content: flex-end;
    gap: 12px;
    padding: 20px;
    border-top: 1px solid #e0e0e0;
}

.cancel-btn,
.confirm-btn {
    padding: 10px 24px;
    border-radius: 6px;
    font-size: 14px;
    cursor: pointer;
    transition: all 0.2s;
}

.cancel-btn {
    background: none;
    border: 1px solid #ddd;
    color: #666;
}

.cancel-btn:hover {
    border-color: #999;
    color: #333;
}

.confirm-btn {
    background-color: #ff6b35;
    border: none;
    color: white;
}

.confirm-btn:hover {
    background-color: #e55a2b;
}

/* Hide default radio buttons */
.address-type-option input[type="radio"] {
    display: none;
}
</style>
