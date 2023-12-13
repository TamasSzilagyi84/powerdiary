"use client";

import { useSearchParams, usePathname, useRouter } from 'next/navigation';

export default function Dropdown ({ value }: { value: string }) {
    const searchParams = useSearchParams();
    const pathname = usePathname();
    const { replace } = useRouter();

    function updateTake(eventValue: string) {
        const params = new URLSearchParams(searchParams);

        value = eventValue;
        params.set('type', value);

        console.log(value);
        replace(`${pathname}?${params.toString()}`);
      }

    return (
        <div className='grid grid-cols-6 mb-6'>
            <div>Aggregation type</div>
            <select value={value.toString()} onChange={(e) => {updateTake(e.target.value)}}>
                <option value="Yearly">Yearly</option>
                <option value="Monthly">Monthly</option>
                <option value="Daily">Daily</option>
                <option value="Hourly">Hourly</option>
                <option value="Minutely">Minutely</option>
            </select>
        </div>        
    );
};

  