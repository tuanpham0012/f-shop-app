<template lang="">
    <div class="modal fade show" style="display: block; padding-right: 15px" aria-modal="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true"><i class="icon-close"></i></span>
                    </button>

                    <div class="form-box">
                        <div class="form-tab">
                            <ul class="nav nav-pills nav-fill" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link" id="signin-tab" data-toggle="tab" href="#signin" role="tab" aria-controls="signin" aria-selected="false">Sign In</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link active" id="register-tab" data-toggle="tab" href="#register" role="tab" aria-controls="register" aria-selected="true">Register</a>
                                </li>
                            </ul>
                            <div class="tab-content px-12" id="tab-content-5">
                                <div class="tab-pane fade" id="signin" role="tabpanel" aria-labelledby="signin-tab">
                                    <form action="#">
                                        <div class="form-group">
                                            <label for="singin-email">Username or email address *</label>
                                            <input type="text" class="form-control" id="singin-email" name="singin-email" required="" />
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-group">
                                            <label for="singin-password">Password *</label>
                                            <input type="password" class="form-control" id="singin-password" name="singin-password" required="" />
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-footer">
                                            <button type="submit" class="btn btn-outline-primary-2">
                                                <span>LOG IN</span>
                                                <i class="icon-long-arrow-right"></i>
                                            </button>

                                            <div class="custom-control custom-checkbox">
                                                <input type="checkbox" class="custom-control-input" id="signin-remember" />
                                                <label class="custom-control-label" for="signin-remember">Remember Me</label>
                                            </div>
                                            <!-- End .custom-checkbox -->

                                            <a href="#" class="forgot-link">Forgot Your Password?</a>
                                        </div>
                                        <!-- End .form-footer -->
                                    </form>
                                    <div class="form-choice">
                                        <p class="text-center">or sign in with</p>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <GoogleLoginComponent />
                                            </div>
                                            <!-- End .col-6 -->
                                            <div class="col-sm-6">
                                                <a href="#" class="btn btn-login btn-f">
                                                    <i class="icon-facebook-f"></i>
                                                    Login With Facebook
                                                </a>
                                            </div>
                                            <!-- End .col-6 -->
                                        </div>
                                        <!-- End .row -->
                                    </div>
                                    <!-- End .form-choice -->
                                </div>
                                <!-- .End .tab-pane -->
                                <div class="tab-pane fade active show" id="register" role="tabpanel" aria-labelledby="register-tab">
                                    <form action="#">
                                        <div class="form-group">
                                            <label for="register-email">Your email address *</label>
                                            <input type="email" class="form-control" id="register-email" name="register-email" required="" />
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-group">
                                            <label for="register-password">Password *</label>
                                            <input type="password" class="form-control" id="register-password" name="register-password" required="" />
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-footer">
                                            <button type="submit" class="btn btn-outline-primary-2">
                                                <span>SIGN UP</span>
                                                <i class="icon-long-arrow-right"></i>
                                            </button>

                                            <div class="flex">
                                                <input type="checkbox" class="custom-control-input" id="register-policy" required="" />
                                                <label class="custom-control-label" for="register-policy">I agree to the <a href="#">privacy policy</a> *</label>
                                            </div>
                                            <!-- End .custom-checkbox -->
                                        </div>
                                        <!-- End .form-footer -->
                                    </form>
                                    <div class="form-choice">
                                        <p class="text-center mb-0">or sign in with</p>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <GoogleLoginComponent />
                                            </div>
                                            <!-- End .col-6 -->
                                            <div class="col-sm-6">
                                                <a href="#" class="btn btn-login btn-f">
                                                    <i class="icon-facebook-f"></i>
                                                    Login With Facebookkk
                                                </a>
                                            </div>
                                            <!-- End .col-6 -->
                                        </div>
                                        <!-- End .row -->
                                    </div>
                                    <!-- End .form-choice -->
                                </div>
                                <!-- .End .tab-pane -->
                            </div>
                            <!-- End .tab-content -->
                        </div>
                        <!-- End .form-tab -->
                    </div>
                    <!-- End .form-box -->
                </div>
                <!-- End .modal-body -->
            </div>
            <!-- End .modal-content -->
        </div>
        <!-- End .modal-dialog -->
    </div>
</template>
<script setup lang="ts">
import GoogleLoginComponent from './GoogleLoginComponent.vue';
</script>
<style scoped lang="scss">
</style>
