import React, { FC, FormEvent, useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { UserClient, UserDto } from "../web-api-client.ts"

type EditProps = {
    buttonLabel: "Create user" | "Update user",
    title: string
}

const Edit: FC<EditProps> = ({ buttonLabel, title }) => {
    const [user, setUser] = useState<UserDto | null>(null)
    const navigation = useNavigate()

    const { uid } = useParams<string>()
    useEffect(() => {
        const handleFetchUser = async () => {
            const client = new UserClient()
            try {
                if (uid) {
                    const response = await client.getUser(uid)
                    setUser(response)
                }
            } catch (error) {
                if (error instanceof TypeError) {
                    console.log(error.message);
                } else if (error instanceof ReferenceError) {
                    console.log(error.message);
                } else {
                    console.log(error);
                }
            }
        }
        if (buttonLabel === "Update user") {
            handleFetchUser()
        }

        return () => {
            setUser(null)
        }
    }, [uid, buttonLabel])

    const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const client = new UserClient()
        try {
            if (buttonLabel === "Update user") {
                const response = await client.updateUserById({
                    userId: user?.id,
                    init: user?.init() as any,
                    toJSON: user?.toJSON(),
                    firstName: user?.firstName,
                    lastName: user?.lastName
                })
                if (response.succeeded) {
                    navigation("/users", { preventScrollReset: true })
                }
            } else if (buttonLabel === "Create user") {
                const response = await client.createUser(
                    {
                        init: user?.init() as any,
                        toJSON: user?.toJSON(),
                        firstName: user?.firstName,
                        lastName: user?.lastName
                    }
                )
                if (response.succeeded) {
                    navigation('/users', { preventScrollReset: true })
                } else {
                    console.log(response.errors);
                }
            }
        } catch (error) {
            if (error instanceof TypeError) {
                console.log(error.message);
            } else if (error instanceof ReferenceError) {
                console.log(error.message);
            } else {
                console.log(error);
            }
        }
    }

    return <>
        <div className="container w-full d-flex justify-content-center align-items-center" style={{ height: "80vh" }}>
            <div className="card w-100" style={{ maxWidth: "400px" }}>
                <div className="card-body">
                    <div className="card-title fs-3 text-center mb-3">{title}</div>
                    <form onSubmit={handleSubmit} >
                        <div className="form-floating mb-3">
                            <input onChange={e => setUser(prev => ({ ...prev, firstName: e.target.value, init: () => { }, toJSON: () => { } }))} value={user?.firstName ?? ""} type="text" name="firstname" id="firstname" className="form-control" placeholder="First name" />
                            <label htmlFor="firstname">First name</label>
                        </div>
                        <div className="form-floating mb-3">
                            <input onChange={e => setUser(prev => ({ ...prev, lastName: e.target.value, init: () => { }, toJSON: () => { } }))} value={user?.lastName ?? ""} type="text" name="lastname" id="lastname" className="form-control" placeholder="Last name" />
                            <label htmlFor="lastname">Last name</label>
                        </div>
                        <button type="submit" className="btn btn-primary w-100">{buttonLabel}</button>
                    </form>
                </div>
            </div>
        </div>
    </>
}

export default Edit