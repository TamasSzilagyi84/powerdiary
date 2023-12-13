"use client";

import { useSearchParams, usePathname, useRouter } from 'next/navigation';

export default function Dropdown ({ value }: { value: Number }) {
    const searchParams = useSearchParams();
    const pathname = usePathname();
    const { replace } = useRouter();

    function updateTake(eventValue: string) {
        const params = new URLSearchParams(searchParams);

        if (eventValue) {
            value = Number(eventValue);
            params.set('take', String(value));
        }

        console.log(value);
        replace(`${pathname}?${params.toString()}`);
      }

    return (
        <div className='grid grid-cols-6 mb-6'>
            <div>Items to display on page</div>
            <select value={value.toString()} onChange={(e) => {updateTake(e.target.value)}}>
                <option value="20">20</option>
                <option value="50">50</option>
                <option value="100">100</option>
            </select>
        </div>        
    );
};

  