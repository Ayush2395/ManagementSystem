import React from "react"
import { User } from "../model/props"
import { useAppState } from "../redux/store.ts"
import moment from "moment"

const list: Record<keyof User, string> = {
    id: "User Id",
    firstName: "First Name",
    lastName: "Last Name",
    roles: "User Role",
    created: "Created At",
}

const UserList = () => {
    const users = useAppState(state => state.userlistSlice)
    return (
        <>
            <table className="table table-responsive">
                <thead>
                    {
                         Object.entries(users).map(([key, value], idx) => {
                            const columnName = list[key]
                            return <tr key={idx}>
                                <th>{columnName}</th>
                            </tr>
                        })
                    }
                </thead>
                <tbody>
                    {
                        Object.values(users).map((value, idx) => <tr>
                            <td>{value.id}</td>
                            <td>{value.firstName}</td>
                            <td>{value.lastName}</td>
                            <td>{value.roles}</td>
                            <td>{moment(value.created).format("lll")}</td>
                        </tr>)
                    }
                </tbody>
            </table>
        </>
    )

}

export default UserList