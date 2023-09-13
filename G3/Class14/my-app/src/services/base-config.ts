export const BASE_URL = "https://localhost:7208"

export function post<T>(url: string, model: T) {
    return fetch(BASE_URL + url, {
        method: 'POST',
        body: JSON.stringify(model),
        headers: {
            "Content-Type": "application/json",
        },
    })
}

export function postAuth<T>(url: string, model: T, token: string) {
    return fetch(BASE_URL + url, {
        method: 'POST',
        body: JSON.stringify(model),
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
    })
}

export function putAuth<T>(url: string, model: T, token: string) {
    return fetch(BASE_URL + url, {
        method: 'PUT',
        body: JSON.stringify(model),
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
    })
}



export async function get<T>(url: string) {
    const response = await fetch(BASE_URL + url, {
        method: 'GET',
        headers: {
            "Content-Type": "application/json",
        },
    })
    return (await response.json()) as T
}

export async function getAuth<T>(url: string, auth: string) {
    const response = await fetch(BASE_URL + url, {
        method: 'GET',
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + auth
        },
    })
    return (await response.json()) as T
}

export async function deleteAuth<T>(url: string, auth: string) {
    const response = await fetch(BASE_URL + url, {
        method: 'DELETE',
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + auth
        },
    })
    return (await response.json()) as T
}