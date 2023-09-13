import { UserLoginModel } from "../models/user-login-model";
import { BASE_URL, post } from "./base-config";

async function Login(model: UserLoginModel) {
    try {
        const response = await post('/api/user/login', model);
        const json = await response.text()
        return json;
    } catch (err) {
        console.log(err);
        return null;
    }
}

export default {
    Login
}