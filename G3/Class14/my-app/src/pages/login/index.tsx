import React, { useState } from 'react';
import { UserLoginModel } from '../../models/user-login-model';
import UserService from '../../services/user-service';
interface ILoginProps {
    onTokenChange: (token: string | null) => void;
}

const Login = (props: ILoginProps) => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const onLogin = () => {
        var userLogin: UserLoginModel = {
            username: email,
            password
        }
        UserService.Login(userLogin).then(x => {
            if (x) {
                sessionStorage.setItem("user", x);
                props.onTokenChange(x)
            }
        })
    }

    return <div className='login-container'>
        <form>
            {email}
            <div>
                <label htmlFor="email">Email</label>
                <input value={email} onChange={(e) => setEmail(e.target.value)} id='email' />
            </div>

            <div>
                <label htmlFor='password'>Password</label>
                <input value={password} onChange={e => setPassword(e.target.value)} type='password' id='password' />
            </div>
            <button type='button' onClick={onLogin}>Login</button>
        </form>
    </div>
}


export default Login;