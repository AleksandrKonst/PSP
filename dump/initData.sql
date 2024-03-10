--
-- PostgreSQL database dump
--

-- Dumped from database version 16.1
-- Dumped by pg_dump version 16.1

-- Started on 2024-03-10 15:46:56

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5 (class 2615 OID 16740)
-- Name: PSP; Type: SCHEMA; Schema: -; Owner: -
--

CREATE SCHEMA "PSP";


--
-- TOC entry 4987 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA "PSP"; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON SCHEMA "PSP" IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 215 (class 1259 OID 16741)
-- Name: data_coupon_events; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".data_coupon_events (
    id bigint NOT NULL,
    operation_type character varying NOT NULL,
    operation_datetime_utc timestamp with time zone NOT NULL,
    operation_datetime_timezone smallint NOT NULL,
    operation_place character varying,
    passenger_id bigint NOT NULL,
    document_type_code character varying NOT NULL,
    document_number character varying NOT NULL,
    document_number_latin character varying NOT NULL,
    quota_code character varying NOT NULL,
    flight_code integer NOT NULL,
    ticket_type smallint NOT NULL,
    ticket_number character varying NOT NULL
);


--
-- TOC entry 4988 (class 0 OID 0)
-- Dependencies: 215
-- Name: TABLE data_coupon_events; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON TABLE "PSP".data_coupon_events IS 'События с купонами';


--
-- TOC entry 4989 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN data_coupon_events.id; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_coupon_events.id IS 'Идентификатор события с купоном';


--
-- TOC entry 4990 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN data_coupon_events.operation_type; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_coupon_events.operation_type IS 'Тип операции с билетами';


--
-- TOC entry 4991 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN data_coupon_events.operation_datetime_utc; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_coupon_events.operation_datetime_utc IS 'Дата и время операции с билетами (UTC)';


--
-- TOC entry 4992 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN data_coupon_events.operation_datetime_timezone; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_coupon_events.operation_datetime_timezone IS 'Временная зона времени операции с билетами';


--
-- TOC entry 4993 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN data_coupon_events.operation_place; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_coupon_events.operation_place IS 'Место проведения операции с билетами';


--
-- TOC entry 4994 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN data_coupon_events.passenger_id; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_coupon_events.passenger_id IS 'Идентификатор пассажира';


--
-- TOC entry 4995 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN data_coupon_events.document_type_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_coupon_events.document_type_code IS 'Тип документа';


--
-- TOC entry 4996 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN data_coupon_events.document_number; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_coupon_events.document_number IS 'Номер документа';


--
-- TOC entry 4997 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN data_coupon_events.document_number_latin; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_coupon_events.document_number_latin IS 'Номер документа latin';


--
-- TOC entry 4998 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN data_coupon_events.quota_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_coupon_events.quota_code IS 'Код квотирования';


--
-- TOC entry 4999 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN data_coupon_events.flight_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_coupon_events.flight_code IS 'Код перелета';


--
-- TOC entry 5000 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN data_coupon_events.ticket_type; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_coupon_events.ticket_type IS 'Тип билета';


--
-- TOC entry 5001 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN data_coupon_events.ticket_number; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_coupon_events.ticket_number IS 'Номер билета';


--
-- TOC entry 216 (class 1259 OID 16746)
-- Name: data_coupon_events_id_seq; Type: SEQUENCE; Schema: PSP; Owner: -
--

CREATE SEQUENCE "PSP".data_coupon_events_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 5002 (class 0 OID 0)
-- Dependencies: 216
-- Name: data_coupon_events_id_seq; Type: SEQUENCE OWNED BY; Schema: PSP; Owner: -
--

ALTER SEQUENCE "PSP".data_coupon_events_id_seq OWNED BY "PSP".data_coupon_events.id;


--
-- TOC entry 217 (class 1259 OID 16747)
-- Name: data_coupon_events_passenger_id_seq; Type: SEQUENCE; Schema: PSP; Owner: -
--

CREATE SEQUENCE "PSP".data_coupon_events_passenger_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 5003 (class 0 OID 0)
-- Dependencies: 217
-- Name: data_coupon_events_passenger_id_seq; Type: SEQUENCE OWNED BY; Schema: PSP; Owner: -
--

ALTER SEQUENCE "PSP".data_coupon_events_passenger_id_seq OWNED BY "PSP".data_coupon_events.passenger_id;


--
-- TOC entry 224 (class 1259 OID 16805)
-- Name: data_flight; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".data_flight (
    code integer NOT NULL,
    airline_code character varying NOT NULL,
    depart_place character varying NOT NULL,
    depart_datetime_plan timestamp with time zone NOT NULL,
    arrive_place character varying NOT NULL,
    arrive_datetime_plan timestamp with time zone NOT NULL,
    pnr_code character varying NOT NULL,
    fare_code character varying NOT NULL
);


--
-- TOC entry 5004 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN data_flight.code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_flight.code IS 'Код маршрута';


--
-- TOC entry 5005 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN data_flight.airline_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_flight.airline_code IS 'Код авиаперевочика';


--
-- TOC entry 5006 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN data_flight.depart_place; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_flight.depart_place IS 'Код аэропорта отправки';


--
-- TOC entry 5007 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN data_flight.depart_datetime_plan; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_flight.depart_datetime_plan IS 'Время отправки';


--
-- TOC entry 5008 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN data_flight.arrive_place; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_flight.arrive_place IS 'Код аэропорта посадки';


--
-- TOC entry 5009 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN data_flight.arrive_datetime_plan; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_flight.arrive_datetime_plan IS 'Время посадки';


--
-- TOC entry 5010 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN data_flight.pnr_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_flight.pnr_code IS 'PNR код';


--
-- TOC entry 5011 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN data_flight.fare_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_flight.fare_code IS 'Код тарифа';


--
-- TOC entry 218 (class 1259 OID 16752)
-- Name: data_passenger; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".data_passenger (
    id bigint NOT NULL,
    name character varying NOT NULL,
    surname character varying NOT NULL,
    patronymic character varying,
    birthdate date NOT NULL,
    gender character varying NOT NULL,
    passenger_types character varying[]
);


--
-- TOC entry 5012 (class 0 OID 0)
-- Dependencies: 218
-- Name: COLUMN data_passenger.id; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_passenger.id IS 'Идентификатор пассажира';


--
-- TOC entry 5013 (class 0 OID 0)
-- Dependencies: 218
-- Name: COLUMN data_passenger.name; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_passenger.name IS 'Имя пассажира';


--
-- TOC entry 5014 (class 0 OID 0)
-- Dependencies: 218
-- Name: COLUMN data_passenger.surname; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_passenger.surname IS 'Фамилия пассажира';


--
-- TOC entry 5015 (class 0 OID 0)
-- Dependencies: 218
-- Name: COLUMN data_passenger.patronymic; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_passenger.patronymic IS 'Отчество пассажира';


--
-- TOC entry 5016 (class 0 OID 0)
-- Dependencies: 218
-- Name: COLUMN data_passenger.birthdate; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_passenger.birthdate IS 'Дата рождения';


--
-- TOC entry 5017 (class 0 OID 0)
-- Dependencies: 218
-- Name: COLUMN data_passenger.gender; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_passenger.gender IS 'Пол пассажира';


--
-- TOC entry 5018 (class 0 OID 0)
-- Dependencies: 218
-- Name: COLUMN data_passenger.passenger_types; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".data_passenger.passenger_types IS 'Типы пассажира';


--
-- TOC entry 219 (class 1259 OID 16757)
-- Name: data_passenger_id_seq; Type: SEQUENCE; Schema: PSP; Owner: -
--

CREATE SEQUENCE "PSP".data_passenger_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 5019 (class 0 OID 0)
-- Dependencies: 219
-- Name: data_passenger_id_seq; Type: SEQUENCE OWNED BY; Schema: PSP; Owner: -
--

ALTER SEQUENCE "PSP".data_passenger_id_seq OWNED BY "PSP".data_passenger.id;


--
-- TOC entry 220 (class 1259 OID 16771)
-- Name: dict_airlines; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".dict_airlines (
    iata_code character varying NOT NULL,
    name_ru character varying NOT NULL,
    name_en character varying NOT NULL,
    icao_code character varying NOT NULL,
    rf_code character varying,
    country character varying NOT NULL,
    transportation_periods tsmultirange,
    reports_use_flight_data_fact boolean NOT NULL,
    reports_use_first_transfer_flight_depart_date boolean NOT NULL,
    CONSTRAINT dict_airlines_check_country CHECK ((TRIM(BOTH FROM country) <> ''::text)),
    CONSTRAINT dict_airlines_check_iata_code CHECK (((iata_code)::text ~ '^[A-Z0-9]{2}$'::text)),
    CONSTRAINT dict_airlines_check_icao_code CHECK (((icao_code)::text ~ '^[A-Z]{3}$'::text)),
    CONSTRAINT dict_airlines_check_name_en CHECK ((TRIM(BOTH FROM name_en) <> ''::text)),
    CONSTRAINT dict_airlines_check_name_ru CHECK ((TRIM(BOTH FROM name_ru) <> ''::text)),
    CONSTRAINT dict_airlines_check_rf_code CHECK (((rf_code IS NULL) OR ((rf_code)::text ~ '^[ЁА-Я0-9]{2}$'::text)))
);


--
-- TOC entry 5020 (class 0 OID 0)
-- Dependencies: 220
-- Name: TABLE dict_airlines; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON TABLE "PSP".dict_airlines IS 'Авиакомпании';


--
-- TOC entry 5021 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN dict_airlines.iata_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airlines.iata_code IS 'Код IATA авиакомпании';


--
-- TOC entry 5022 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN dict_airlines.name_ru; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airlines.name_ru IS 'Название авиакомпании (рус.)';


--
-- TOC entry 5023 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN dict_airlines.name_en; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airlines.name_en IS 'Название авиакомпании (англ.)';


--
-- TOC entry 5024 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN dict_airlines.icao_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airlines.icao_code IS 'Код ICAO авиакомпании';


--
-- TOC entry 5025 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN dict_airlines.rf_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airlines.rf_code IS 'Код авиакомпании по Воздушному кодексу РФ';


--
-- TOC entry 5026 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN dict_airlines.country; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airlines.country IS 'Страна авиакомпании';


--
-- TOC entry 5027 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN dict_airlines.transportation_periods; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airlines.transportation_periods IS 'Список периодов выполнения авиакомпанией субсидированных перевозок';


--
-- TOC entry 5028 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN dict_airlines.reports_use_flight_data_fact; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airlines.reports_use_flight_data_fact IS 'Авиакомпания сдает отчеты в Росавиацию по фактическим данным рейсов';


--
-- TOC entry 5029 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN dict_airlines.reports_use_first_transfer_flight_depart_date; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airlines.reports_use_first_transfer_flight_depart_date IS 'Авиакомпания отбирает в отчеты в Росавиацию трансферные перевозки по дате первого рейса';


--
-- TOC entry 221 (class 1259 OID 16782)
-- Name: dict_airports; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".dict_airports (
    iata_code character varying NOT NULL,
    icao_code character varying NOT NULL,
    rf_code character varying,
    name character varying NOT NULL,
    city_iata_code character varying NOT NULL,
    latitude numeric,
    longitude numeric,
    CONSTRAINT dict_airports_check_iata_code CHECK (((iata_code)::text ~ '^[A-Z]{3}$'::text)),
    CONSTRAINT dict_airports_check_icao_code CHECK (((icao_code)::text ~ '^[A-Z]{4}$'::text)),
    CONSTRAINT dict_airports_check_name CHECK ((TRIM(BOTH FROM name) <> ''::text)),
    CONSTRAINT dict_airports_check_rf_code CHECK (((rf_code IS NULL) OR ((rf_code)::text ~ '^[ЁА-Я]{3}$'::text)))
);


--
-- TOC entry 5030 (class 0 OID 0)
-- Dependencies: 221
-- Name: TABLE dict_airports; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON TABLE "PSP".dict_airports IS 'Аэропорты';


--
-- TOC entry 5031 (class 0 OID 0)
-- Dependencies: 221
-- Name: COLUMN dict_airports.iata_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airports.iata_code IS 'Код IATA аэропорта';


--
-- TOC entry 5032 (class 0 OID 0)
-- Dependencies: 221
-- Name: COLUMN dict_airports.icao_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airports.icao_code IS 'Код ICAO аэропорта';


--
-- TOC entry 5033 (class 0 OID 0)
-- Dependencies: 221
-- Name: COLUMN dict_airports.rf_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airports.rf_code IS 'Код аэропорта по Воздушному кодексу РФ';


--
-- TOC entry 5034 (class 0 OID 0)
-- Dependencies: 221
-- Name: COLUMN dict_airports.name; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airports.name IS 'Название аэропорта';


--
-- TOC entry 5035 (class 0 OID 0)
-- Dependencies: 221
-- Name: COLUMN dict_airports.city_iata_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airports.city_iata_code IS 'Ссылка на населенный пункт расположения аэропорта';


--
-- TOC entry 5036 (class 0 OID 0)
-- Dependencies: 221
-- Name: COLUMN dict_airports.latitude; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airports.latitude IS 'Широта';


--
-- TOC entry 5037 (class 0 OID 0)
-- Dependencies: 221
-- Name: COLUMN dict_airports.longitude; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_airports.longitude IS 'Долгота';


--
-- TOC entry 222 (class 1259 OID 16791)
-- Name: dict_cities; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".dict_cities (
    iata_code character varying NOT NULL,
    name character varying NOT NULL,
    CONSTRAINT dict_cities_check_iata_code CHECK (((iata_code)::text ~ '^[A-Z]{3}$'::text)),
    CONSTRAINT dict_cities_check_name CHECK ((TRIM(BOTH FROM name) <> ''::text))
);


--
-- TOC entry 5038 (class 0 OID 0)
-- Dependencies: 222
-- Name: TABLE dict_cities; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON TABLE "PSP".dict_cities IS 'Населенные пункты';


--
-- TOC entry 5039 (class 0 OID 0)
-- Dependencies: 222
-- Name: COLUMN dict_cities.iata_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_cities.iata_code IS 'Код IATA населенного пункта';


--
-- TOC entry 5040 (class 0 OID 0)
-- Dependencies: 222
-- Name: COLUMN dict_cities.name; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_cities.name IS 'Название населенного пункта';


--
-- TOC entry 223 (class 1259 OID 16798)
-- Name: dict_document_types; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".dict_document_types (
    code character varying NOT NULL,
    type character varying NOT NULL,
    CONSTRAINT dict_document_types_check_code CHECK (((code)::text ~ '^\d{2}$'::text)),
    CONSTRAINT dict_document_types_check_type CHECK ((TRIM(BOTH FROM type) <> ''::text))
);


--
-- TOC entry 5041 (class 0 OID 0)
-- Dependencies: 223
-- Name: TABLE dict_document_types; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON TABLE "PSP".dict_document_types IS 'Типы документов, удостоверяющих личность';


--
-- TOC entry 5042 (class 0 OID 0)
-- Dependencies: 223
-- Name: COLUMN dict_document_types.code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_document_types.code IS 'Код типа документа, удостоверяющего личность';


--
-- TOC entry 5043 (class 0 OID 0)
-- Dependencies: 223
-- Name: COLUMN dict_document_types.type; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_document_types.type IS 'Тип документа, удостоверяющего личность';


--
-- TOC entry 232 (class 1259 OID 17202)
-- Name: dict_fare; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".dict_fare (
    code character varying NOT NULL,
    amount numeric NOT NULL,
    currency character varying NOT NULL,
    special boolean NOT NULL
);


--
-- TOC entry 5044 (class 0 OID 0)
-- Dependencies: 232
-- Name: COLUMN dict_fare.code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_fare.code IS 'Идендификатор тарифа';


--
-- TOC entry 5045 (class 0 OID 0)
-- Dependencies: 232
-- Name: COLUMN dict_fare.amount; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_fare.amount IS 'Стоимость тарифа';


--
-- TOC entry 5046 (class 0 OID 0)
-- Dependencies: 232
-- Name: COLUMN dict_fare.currency; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_fare.currency IS 'Валюта';


--
-- TOC entry 5047 (class 0 OID 0)
-- Dependencies: 232
-- Name: COLUMN dict_fare.special; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_fare.special IS 'Специальный тариф';


--
-- TOC entry 225 (class 1259 OID 16810)
-- Name: dict_genders; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".dict_genders (
    code character varying NOT NULL,
    gender character varying NOT NULL,
    CONSTRAINT dict_genders_check_code CHECK (((TRIM(BOTH FROM code) <> ''::text) AND (upper((code)::text) = (code)::text))),
    CONSTRAINT dict_genders_check_gender CHECK ((TRIM(BOTH FROM gender) <> ''::text))
);


--
-- TOC entry 5048 (class 0 OID 0)
-- Dependencies: 225
-- Name: TABLE dict_genders; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON TABLE "PSP".dict_genders IS 'Полы';


--
-- TOC entry 5049 (class 0 OID 0)
-- Dependencies: 225
-- Name: COLUMN dict_genders.code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_genders.code IS 'Код пола';


--
-- TOC entry 5050 (class 0 OID 0)
-- Dependencies: 225
-- Name: COLUMN dict_genders.gender; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_genders.gender IS 'Пол';


--
-- TOC entry 231 (class 1259 OID 17184)
-- Name: dict_operation_type; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".dict_operation_type (
    code character varying NOT NULL,
    operation_description character varying
);


--
-- TOC entry 5051 (class 0 OID 0)
-- Dependencies: 231
-- Name: COLUMN dict_operation_type.code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_operation_type.code IS 'Код операции';


--
-- TOC entry 5052 (class 0 OID 0)
-- Dependencies: 231
-- Name: COLUMN dict_operation_type.operation_description; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_operation_type.operation_description IS 'Описание операции';


--
-- TOC entry 226 (class 1259 OID 16817)
-- Name: dict_passenger_types; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".dict_passenger_types (
    code character varying NOT NULL,
    type character varying NOT NULL,
    description character varying NOT NULL,
    appendices smallint[] NOT NULL,
    quota_categories character varying[],
    CONSTRAINT dict_passenger_types_check_appendices CHECK ((cardinality(appendices) > 0)),
    CONSTRAINT dict_passenger_types_check_code CHECK (((TRIM(BOTH FROM code) <> ''::text) AND (lower((code)::text) = (code)::text))),
    CONSTRAINT dict_passenger_types_check_description CHECK ((TRIM(BOTH FROM description) <> ''::text)),
    CONSTRAINT dict_passenger_types_check_type CHECK ((TRIM(BOTH FROM type) <> ''::text))
);


--
-- TOC entry 5053 (class 0 OID 0)
-- Dependencies: 226
-- Name: TABLE dict_passenger_types; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON TABLE "PSP".dict_passenger_types IS 'Типы пассажиров';


--
-- TOC entry 5054 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN dict_passenger_types.code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_passenger_types.code IS 'Код типа пассажира';


--
-- TOC entry 5055 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN dict_passenger_types.type; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_passenger_types.type IS 'Тип пассажира';


--
-- TOC entry 5056 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN dict_passenger_types.description; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_passenger_types.description IS 'Описание типа пассажира';


--
-- TOC entry 5057 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN dict_passenger_types.appendices; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_passenger_types.appendices IS 'Список номеров приложений к Постановлению Правительства РФ №215';


--
-- TOC entry 5058 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN dict_passenger_types.quota_categories; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_passenger_types.quota_categories IS 'Список ссылок на категории квотирования';


--
-- TOC entry 227 (class 1259 OID 16826)
-- Name: dict_quota_categories; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".dict_quota_categories (
    code character varying NOT NULL,
    category character varying NOT NULL,
    appendices smallint[] NOT NULL,
    one_way_quota smallint NOT NULL,
    round_trip_quota smallint NOT NULL,
    CONSTRAINT dict_quota_categories_check_appendices CHECK ((cardinality(appendices) > 0)),
    CONSTRAINT dict_quota_categories_check_category CHECK ((TRIM(BOTH FROM category) <> ''::text)),
    CONSTRAINT dict_quota_categories_check_code CHECK (((TRIM(BOTH FROM code) <> ''::text) AND (lower((code)::text) = (code)::text))),
    CONSTRAINT dict_quota_categories_check_one_way_quota CHECK ((one_way_quota >= 0)),
    CONSTRAINT dict_quota_categories_check_round_trip_quota CHECK ((round_trip_quota >= 0))
);


--
-- TOC entry 5059 (class 0 OID 0)
-- Dependencies: 227
-- Name: TABLE dict_quota_categories; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON TABLE "PSP".dict_quota_categories IS 'Категории квотирования';


--
-- TOC entry 5060 (class 0 OID 0)
-- Dependencies: 227
-- Name: COLUMN dict_quota_categories.code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_quota_categories.code IS 'Код категории квотирования';


--
-- TOC entry 5061 (class 0 OID 0)
-- Dependencies: 227
-- Name: COLUMN dict_quota_categories.category; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_quota_categories.category IS 'Категория квотирования';


--
-- TOC entry 5062 (class 0 OID 0)
-- Dependencies: 227
-- Name: COLUMN dict_quota_categories.appendices; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_quota_categories.appendices IS 'Список номеров приложения к Постановлению Правительства РФ №215';


--
-- TOC entry 5063 (class 0 OID 0)
-- Dependencies: 227
-- Name: COLUMN dict_quota_categories.one_way_quota; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_quota_categories.one_way_quota IS 'Квота в одном направлении';


--
-- TOC entry 5064 (class 0 OID 0)
-- Dependencies: 227
-- Name: COLUMN dict_quota_categories.round_trip_quota; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_quota_categories.round_trip_quota IS 'Квота в направлении туда и обратно';


--
-- TOC entry 228 (class 1259 OID 16836)
-- Name: dict_subsidized_routes; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".dict_subsidized_routes (
    id integer NOT NULL,
    city_start_iata_code character varying NOT NULL,
    city_finish_iata_code character varying NOT NULL,
    appendix smallint NOT NULL,
    fare_amount integer NOT NULL,
    subsidy_amount integer NOT NULL,
    currency character varying NOT NULL,
    validity_from timestamp without time zone NOT NULL,
    validity_to timestamp without time zone NOT NULL,
    interior_cities character varying[],
    CONSTRAINT dict_subsidized_routes_check_appendix CHECK ((appendix >= 1)),
    CONSTRAINT dict_subsidized_routes_check_currency CHECK (((currency)::text = 'RUB'::text)),
    CONSTRAINT dict_subsidized_routes_check_fare_amount CHECK ((fare_amount >= 0)),
    CONSTRAINT dict_subsidized_routes_check_subsidy_amount CHECK ((subsidy_amount >= 0)),
    CONSTRAINT dict_subsidized_routes_check_validity_from_validity_to CHECK ((validity_from <= validity_to))
);


--
-- TOC entry 5065 (class 0 OID 0)
-- Dependencies: 228
-- Name: TABLE dict_subsidized_routes; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON TABLE "PSP".dict_subsidized_routes IS 'Субсидированные направления';


--
-- TOC entry 5066 (class 0 OID 0)
-- Dependencies: 228
-- Name: COLUMN dict_subsidized_routes.id; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_subsidized_routes.id IS 'Идентификатор субсидированного направления';


--
-- TOC entry 5067 (class 0 OID 0)
-- Dependencies: 228
-- Name: COLUMN dict_subsidized_routes.city_start_iata_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_subsidized_routes.city_start_iata_code IS 'Ссылка на начальный населенный пункт направления';


--
-- TOC entry 5068 (class 0 OID 0)
-- Dependencies: 228
-- Name: COLUMN dict_subsidized_routes.city_finish_iata_code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_subsidized_routes.city_finish_iata_code IS 'Ссылка на конечный населенный пункт направления';


--
-- TOC entry 5069 (class 0 OID 0)
-- Dependencies: 228
-- Name: COLUMN dict_subsidized_routes.appendix; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_subsidized_routes.appendix IS 'Номер приложения к Постановлению Правительства РФ №215';


--
-- TOC entry 5070 (class 0 OID 0)
-- Dependencies: 228
-- Name: COLUMN dict_subsidized_routes.fare_amount; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_subsidized_routes.fare_amount IS 'Размер специального тарифа для направления';


--
-- TOC entry 5071 (class 0 OID 0)
-- Dependencies: 228
-- Name: COLUMN dict_subsidized_routes.subsidy_amount; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_subsidized_routes.subsidy_amount IS 'Размер субсидии для направления';


--
-- TOC entry 5072 (class 0 OID 0)
-- Dependencies: 228
-- Name: COLUMN dict_subsidized_routes.currency; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_subsidized_routes.currency IS 'Валюта';


--
-- TOC entry 5073 (class 0 OID 0)
-- Dependencies: 228
-- Name: COLUMN dict_subsidized_routes.validity_from; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_subsidized_routes.validity_from IS 'Начало периода актуальности записи';


--
-- TOC entry 5074 (class 0 OID 0)
-- Dependencies: 228
-- Name: COLUMN dict_subsidized_routes.validity_to; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_subsidized_routes.validity_to IS 'Окончание периода актуальности записи';


--
-- TOC entry 5075 (class 0 OID 0)
-- Dependencies: 228
-- Name: COLUMN dict_subsidized_routes.interior_cities; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_subsidized_routes.interior_cities IS 'Список промежуточных населенных пунктов направления';


--
-- TOC entry 229 (class 1259 OID 16846)
-- Name: dict_subsidized_routes_id_seq; Type: SEQUENCE; Schema: PSP; Owner: -
--

CREATE SEQUENCE "PSP".dict_subsidized_routes_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 5076 (class 0 OID 0)
-- Dependencies: 229
-- Name: dict_subsidized_routes_id_seq; Type: SEQUENCE OWNED BY; Schema: PSP; Owner: -
--

ALTER SEQUENCE "PSP".dict_subsidized_routes_id_seq OWNED BY "PSP".dict_subsidized_routes.id;


--
-- TOC entry 230 (class 1259 OID 16847)
-- Name: dict_ticket_types; Type: TABLE; Schema: PSP; Owner: -
--

CREATE TABLE "PSP".dict_ticket_types (
    code smallint NOT NULL,
    type character varying NOT NULL,
    CONSTRAINT dict_ticket_types_check_code CHECK ((code >= 0)),
    CONSTRAINT dict_ticket_types_check_type CHECK ((TRIM(BOTH FROM type) <> ''::text))
);


--
-- TOC entry 5077 (class 0 OID 0)
-- Dependencies: 230
-- Name: TABLE dict_ticket_types; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON TABLE "PSP".dict_ticket_types IS 'Типы перевозочных документов';


--
-- TOC entry 5078 (class 0 OID 0)
-- Dependencies: 230
-- Name: COLUMN dict_ticket_types.code; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_ticket_types.code IS 'Код типа перевозочного документа';


--
-- TOC entry 5079 (class 0 OID 0)
-- Dependencies: 230
-- Name: COLUMN dict_ticket_types.type; Type: COMMENT; Schema: PSP; Owner: -
--

COMMENT ON COLUMN "PSP".dict_ticket_types.type IS 'Тип перевозочного документа';


--
-- TOC entry 4743 (class 2604 OID 16854)
-- Name: data_coupon_events id; Type: DEFAULT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_coupon_events ALTER COLUMN id SET DEFAULT nextval('"PSP".data_coupon_events_id_seq'::regclass);


--
-- TOC entry 4744 (class 2604 OID 16855)
-- Name: data_coupon_events passenger_id; Type: DEFAULT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_coupon_events ALTER COLUMN passenger_id SET DEFAULT nextval('"PSP".data_coupon_events_passenger_id_seq'::regclass);


--
-- TOC entry 4745 (class 2604 OID 16857)
-- Name: data_passenger id; Type: DEFAULT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_passenger ALTER COLUMN id SET DEFAULT nextval('"PSP".data_passenger_id_seq'::regclass);


--
-- TOC entry 4746 (class 2604 OID 16860)
-- Name: dict_subsidized_routes id; Type: DEFAULT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_subsidized_routes ALTER COLUMN id SET DEFAULT nextval('"PSP".dict_subsidized_routes_id_seq'::regclass);


--
-- TOC entry 4964 (class 0 OID 16741)
-- Dependencies: 215
-- Data for Name: data_coupon_events; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".data_coupon_events VALUES (30, 'issued', '2023-03-01 16:10:00+03', 3, 'AVIA CENTER LLC (MOSCOW)', 18, '01', '46464565656', '46464565656', 'invalid', 179, 1, '2344555790');


--
-- TOC entry 4973 (class 0 OID 16805)
-- Dependencies: 224
-- Data for Name: data_flight; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".data_flight VALUES (180, 'SU', 'ABA', '2023-03-01 18:10:00+03', 'SVO', '2023-03-01 19:10:00+03', 'ABA', '5NR');
INSERT INTO "PSP".data_flight VALUES (179, '5N', 'VVO', '2023-03-01 16:10:00+03', 'ABA', '2023-03-01 17:10:00+03', 'ABA', '5NR');


--
-- TOC entry 4967 (class 0 OID 16752)
-- Dependencies: 218
-- Data for Name: data_passenger; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".data_passenger VALUES (5, 'test', 'test', 'test', '2007-07-18', 'M', '{ocean}');
INSERT INTO "PSP".data_passenger VALUES (6, 'ккк', 'кк', 'ааа', '2002-07-18', 'M', '{ocean}');
INSERT INTO "PSP".data_passenger VALUES (7, 'Константинов', 'Константинов', 'Константинов', '2002-07-18', 'M', '{ocean}');
INSERT INTO "PSP".data_passenger VALUES (8, 'Константинов', 'Константинов', 'Константинов', '2002-07-18', 'M', '{ocean}');
INSERT INTO "PSP".data_passenger VALUES (9, 'Константинов', 'Константинов', 'Константинов', '2002-07-18', 'M', '{ocean}');
INSERT INTO "PSP".data_passenger VALUES (2, 'Константинов', 'Константинов', 'Константинов', '2002-07-18', 'M', '{ocean}');
INSERT INTO "PSP".data_passenger VALUES (12, 'риша', 'Константинов', 'Константинов', '2002-07-18', 'M', '{ocean,large}');
INSERT INTO "PSP".data_passenger VALUES (1, 'Гриша', 'Константинов', 'Константинов', '2002-07-18', 'M', '{ocean,large}');
INSERT INTO "PSP".data_passenger VALUES (13, 'Костя', 'Тростенюк', 'Константинов', '2002-07-18', 'M', '{ocean,large}');
INSERT INTO "PSP".data_passenger VALUES (14, 'Костя', 'Иванов', 'Геннадьевич', '2002-07-18', 'M', '{ocean,large}');
INSERT INTO "PSP".data_passenger VALUES (18, 'Виталик', 'Райко', 'Дмитриевич', '2002-07-18', 'M', '{invalid_23}');
INSERT INTO "PSP".data_passenger VALUES (19, 'Геннадий', 'Константинов', 'Геннадьевич', '2002-07-18', 'M', '{ocean,large}');


--
-- TOC entry 4969 (class 0 OID 16771)
-- Dependencies: 220
-- Data for Name: dict_airlines; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".dict_airlines VALUES ('A4', 'АО «Авиакомпания АЗИМУТ»', 'Azimuth Airlines', 'AZO', 'А4', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('EO', 'ООО «Авиакомпания «Икар»', 'LLC "IKAR"', 'KAR', 'АЬ', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('FV', 'АО «Авиакомпания «Россия»', '"Rossiya airlines" JSC', 'SDM', 'ПЛ', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('S7', 'АО «Авиакомпания «Сибирь»', 'JSC Siberia Airlines', 'SBI', 'С7', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"],["2023-01-01 00:00:00","2023-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('R3', 'АО «Авиакомпания «Якутия»', 'Yakutia Airlines', 'SYL', 'ЯК', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"],["2023-01-01 00:00:00","2023-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('6R', 'АО «Авиакомпания АЛРОСА»', 'ALROSA Air Company', 'DRU', 'ЯМ', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"],["2023-01-01 00:00:00","2023-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('Y7', 'АО «АК «НордСтар»', 'JSC "NordStar Airlines"', 'TYA', 'ТИ', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"],["2023-01-01 00:00:00","2023-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('5N', 'АО «АК Смартавиа»', 'JSC "Smartavia AIrlines"', 'AUL', '5Н', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"],["2023-01-01 00:00:00","2023-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('IO', 'АО «Авиакомпания «ИрАэро»', 'JSC "IrAero" Airlines', 'IAE', 'РД', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"],["2023-01-01 00:00:00","2023-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('WZ', 'АО «Ред Вингс»', 'JSC "Red Wings"', 'RWZ', 'ИН', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"],["2023-01-01 00:00:00","2023-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('U6', 'ОАО АК «Уральские авиалинии»', 'JSC "Ural Airlines"', 'SVR', 'У6', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"],["2023-01-01 00:00:00","2023-12-31 23:59:59"]}', false, false);
INSERT INTO "PSP".dict_airlines VALUES ('N4', 'ООО «Северный Ветер»', 'LLC "NORD WIND"', 'NWS', 'КЛ', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('UT', 'ПАО «Авиакомпания «ЮТэйр»', 'UTair Aviation, JSC', 'UTA', 'ЮТ', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"],["2023-01-01 00:00:00","2023-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('SU', 'ПАО «Аэрофлот»', 'PJSC "Aeroflot"', 'AFL', 'СУ', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"],["2023-01-01 00:00:00","2023-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('I8', 'АО «Ижавиа»', 'Izhavia', 'IZA', 'ИЖ', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('F7', 'ООО «АЙ ФЛАЙ»', 'iFly', 'RSY', 'ФЛ', 'Россия', '{["2022-01-01 00:00:00","2022-12-31 23:59:59"],["2023-01-01 00:00:00","2023-12-31 23:59:59"]}', true, true);
INSERT INTO "PSP".dict_airlines VALUES ('D2', 'ООО «Авиапредприятие «Северсталь»', 'Severstal Aircompany', 'SSF', 'Д2', 'Россия', NULL, true, true);
INSERT INTO "PSP".dict_airlines VALUES ('DP', 'ООО «Авиакомпания Победа»', 'Pobeda Airlines LLC', 'PBD', 'ДР', 'Россия', NULL, true, true);
INSERT INTO "PSP".dict_airlines VALUES ('HZ', 'АО «Авиакомпания «Аврора»', 'Aurora Airlines', 'SHU', 'ИЕ', 'Россия', NULL, true, true);
INSERT INTO "PSP".dict_airlines VALUES ('KV', 'АО «КрасАвиа»', 'KRASAVIA', 'SSJ', 'КЯ', 'Россия', NULL, true, true);
INSERT INTO "PSP".dict_airlines VALUES ('PI', 'АО «АК «Полярные авиалинии»', 'AIR SAKHA', 'RKA', 'ЯП', 'Россия', NULL, true, true);


--
-- TOC entry 4970 (class 0 OID 16782)
-- Dependencies: 221
-- Data for Name: dict_airports; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".dict_airports VALUES ('SVO', 'UUEE', 'ШРМ', 'Шереметьево', 'MOW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('AER', 'URSS', 'СОЧ', 'Адлер-Сочи', 'AER', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ARH', 'ULAA', 'АРХ', 'Талаги', 'ARH', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ASF', 'URWA', 'АСР', 'Астрахань', 'ASF', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BKA', 'UUBB', 'БКВ', 'Быково', 'MOW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BQS', 'UHBB', 'БГЩ', 'Игнатьево', 'BQS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BTK', 'UIBB', 'БРС', 'Братск', 'BTK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('CEK', 'USCC', 'ЧЛБ', 'Челябинск', 'CEK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('CSY', 'UWKS', 'ЧБЕ', 'Чебоксары', 'CSY', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('DYR', 'UHMA', 'АНЫ', 'Угольный', 'DYR', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('EGO', 'UUOB', 'БЕД', 'Белгород', 'EGO', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ESL', 'URWI', 'ЭЛИ', 'Элиста', 'ESL', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('GDX', 'UHMM', 'МДС', 'Сокол', 'GDX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('GOJ', 'UWGG', 'НЖС', 'Стригино', 'GOJ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('GRV', 'URMG', 'ГРН', 'Грозный', 'GRV', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('HTA', 'UIAA', 'СХТ', 'Кадала', 'HTA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('IAR', 'UUDL', 'ЯРТ', 'Туношна', 'IAR', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('IJK', 'USII', 'ИЖВ', 'Ижевск', 'IJK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('IKS', 'UEST', 'ТСИ', 'Тикси', 'IKS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('INA', 'UUYI', 'ИНТ', 'Инта', 'INA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('IWA', 'UUBI', 'ИВВ', 'Иваново-Южный', 'IWA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KEJ', 'UNEE', 'КРВ', 'Кемерово', 'KEJ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KGD', 'UMKK', 'КЛД', 'Храброво', 'KGD', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KGP', 'USRK', 'КОГ', 'Когалым', 'KGP', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KJA', 'UNKL', 'ЕМВ', 'Емельяново', 'KJA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KMW', 'UUBA', 'КОР', 'Секеркино', 'KMW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KRO', 'USUU', 'КГН', 'Курган', 'KRO', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KRR', 'URKK', 'КПА', 'Пашковский', 'KRR', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KSZ', 'ULKK', 'КТС', 'Котлас', 'KSZ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KVK', 'ULMK', 'АПХ', 'Апатиты-Кировск', 'WZA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KVX', 'USKK', 'КИО', 'Победилово', 'KVX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KXK', 'UHKK', 'КСЛ', 'Хурба', 'KXK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KZN', 'UWKD', 'КЗН', 'Казань', 'KZN', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('LED', 'ULLI', 'ПЛК', 'Пулково', 'LED', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('LPK', 'UUOL', 'ЛИП', 'Липецк', 'LPK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('MCX', 'URML', 'МХЛ', 'Уйташ', 'MCX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('MJZ', 'UERR', 'МИР', 'Мирный', 'MJZ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('MQF', 'USCM', 'МГС', 'Магнитогорск', 'MQF', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('MRV', 'URMM', 'МРВ', 'Минеральные Воды', 'MRV', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NAL', 'URMN', 'НЧК', 'Нальчик', 'NAL', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NNM', 'ULAM', 'ННР', 'Нарьян-Мар', 'NNM', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NOJ', 'USRO', 'НОЯ', 'Ноябрьск', 'NOJ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NOZ', 'UNWW', 'НВК', 'Спиченково', 'NOZ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NSK', 'UOOO', 'НАК', 'Алыкель', 'NSK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NYM', 'USMM', 'НДМ', 'Надым', 'NYM', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('OEL', 'UUOR', 'ОЕЛ', 'Орёл-Южный', 'OEL', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('OGZ', 'URMO', 'ВЛА', 'Беслан', 'OGZ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('OKT', 'UWUK', 'ОКТ', 'Октябрьский', 'OKT', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('OMS', 'UNOO', 'ОМС', 'Омск-Центральный', 'OMS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('OSW', 'UWOR', 'ОСК', 'Орск', 'OSW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('PEE', 'USPP', 'ПРЬ', 'Большое Савино', 'PEE', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('PES', 'ULPB', 'ПТБ', 'Бесовец', 'PES', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('PEX', 'UUYP', 'ПЧР', 'Печора', 'PEX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('PKC', 'UHPP', 'ПРЛ', 'Елизово', 'PKC', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('PKV', 'ULOO', 'ПСК', 'Псков', 'PKV', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('RAT', 'USNR', 'РАД', 'Радужный', 'RAT', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('REN', 'UWOO', 'ОНГ', 'Оренбург-Центральный', 'REN', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ROV', 'URRR', 'РОВ', 'Платов', 'ROV', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('RYB', 'UUBK', 'РБН', 'Староселье', 'RYB', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('RZN', 'UUBR', 'РЯТ', 'Турлатово', 'RZN', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SCW', 'UUYY', 'СЫВ', 'Сыктывкар', 'SCW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SKX', 'UWPS', 'СРН', 'Саранск', 'SKX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SLY', 'USDD', 'СХД', 'Салехард', 'SLY', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('STW', 'URMT', 'СТВ', 'Шпаковское', 'STW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SVX', 'USSS', 'КЛЦ', 'Кольцово', 'SVX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('TJM', 'USTR', 'РЩН', 'Рощино', 'TJM', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('TOF', 'UNTT', 'ТСК', 'Богашево', 'TOF', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('TOX', 'USTO', 'ТБЛ', 'Тобольск', 'TOX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('UCT', 'UUYH', 'УХТ', 'Ухта', 'UCT', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('UFA', 'UWUU', 'УФА', 'Уфа', 'UFA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('UKX', 'UITT', 'УСК', 'Усть-Кут', 'UKX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('URS', 'UUOK', 'КУС', 'Восточный', 'KUR', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('USK', 'UUYS', 'УСН', 'Усинск', 'USK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('UUA', 'UWKB', 'БУГ', 'Бугульма', 'UUA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('UUS', 'UHSS', 'ЮЖХ', 'Хомутово', 'UUS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('VGD', 'ULWW', 'ВГД', 'Вологда', 'VGD', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('VKO', 'UUWW', 'ВНК', 'Внуково', 'MOW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('VLU', 'ULOL', 'ВЕК', 'Великие Луки', 'VLU', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('VOG', 'URWW', 'ВГГ', 'Гумрак', 'VOG', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('VOZ', 'UUOO', 'ВРН', 'Чертовицкое', 'VOZ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('VVO', 'UHWW', 'ВВО', 'Кневичи', 'VVO', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('YKS', 'UEEE', 'ЯКТ', 'Якутск', 'YKS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('RGK', 'UNBG', 'ГОР', 'Горно-Алтайск', 'RGK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ULK', 'UERL', 'ЛСК', 'Ленск', 'ULK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NBC', 'UWKE', 'НЖК', 'Бегишево', 'NBC', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('OLZ', 'UEMO', 'ОЛК', 'Олекминск', 'OLZ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('OHH', 'UHSH', 'ОХА', 'Оха', 'OHH', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ABA', 'UNAA', 'АБН', 'Абакан', 'ABA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BAX', 'UNBB', 'БАН', 'Барнаул', 'BAX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('CEE', 'ULWC', 'ЧРВ', 'Череповец', 'CEE', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('DME', 'UUDD', 'ДМД', 'Домодедово', 'MOW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('GDZ', 'URKG', 'ГДЖ', 'Геленджик', 'GDZ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('HMA', 'USHH', 'ХАС', 'Ханты-Мансийск', 'WZE', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('IKT', 'UIII', 'ИКТ', 'Иркутск', 'IKT', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ZIA', 'UUBW', 'РНЦ', 'Жуковский', 'ZIA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BZK', 'UUBP', 'БРЯ', 'Брянск', 'BZK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KLF', 'UUBC', 'КЛГ', 'Калуга', 'KLF', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BGN', 'UESG', 'БЯГ', 'Белая Гора', 'BGN', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BQJ', 'UEBB', 'БТГ', 'Батагай', 'BQJ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('CKH', 'UESO', 'ЧКД', 'Чокурдах', 'CKH', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('EVN', 'UDYZ', NULL, 'Ереван', 'EVN', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('FRU', 'UCFM', NULL, 'Бишкек', 'FRU', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('GSV', 'UWSG', 'ГСВ', 'Гагарин', 'RTW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('IAA', 'UOII', 'ИГР', 'Игарка', 'IAA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ITU', 'UHSI', 'ККУ', 'Итуруп', 'UUS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('MSQ', 'UMMS', NULL, 'Минск', 'MSQ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NQZ', 'UACC', NULL, 'Нурсултан', 'TSE', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('UKG', 'UEBT', 'УКУ', 'Усть-Куйга', 'UKG', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('USR', 'UEMT', 'УНР', 'Усть-Нера', 'USR', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('AMV', 'ULDD', 'АМД', 'Амдерма', 'AMV', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ACS', 'UNKS', NULL, 'Ачинск', 'ACS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('DKS', 'UODD', 'ДИК', 'Диксон', 'DKS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KVR', 'UHWK', 'КАВ', 'Кавалерово', 'KVR', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KLD', 'UUEM', NULL, 'Мигалково', 'TVE', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('LDG', 'ULAL', 'ЛЕШ', 'Лешуконское', 'LDG', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NFG', 'USRN', NULL, 'Нефтеюганск', 'NFG', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NYA', 'USHN', 'НЯГ', 'Нягань', 'NYA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SEV', 'UKCS', 'СЕД', 'Северодонецк', 'SEV', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('CSH', 'ULAS', 'СОИ', 'Соловки', 'CSH', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SWT', 'UNSS', 'СТЖ', 'Стрежевой', 'SWT', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('TYA', 'UUBT', NULL, 'Клоково', 'TYA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('URJ', 'USHU', 'УРА', 'Урай', 'URJ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('HTG', 'UOHH', 'ХАТ', 'Хатанга', 'HTG', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('CYX', 'UESS', 'ЧРС', 'Черский', 'CYX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('GNJ', 'UBBG', NULL, 'Гянджа', 'KVD', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('LWN', 'UDSG', NULL, ' Гюмри Ширак', 'LWN', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BQT', 'UMBB', 'БРТ', 'Брест', 'BQT', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('VTB', 'UMII', 'ВИТ', 'Восточный', 'VTB', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('GNA', 'UMMG', 'ГРД', 'Гродно', 'GNA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('MVQ', 'UMOO', 'МГЛ', 'Могилев', 'MVQ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SCO', 'UATE', NULL, 'Актау', 'SCO', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('GUW', 'UATG', NULL, 'Атырау', 'GUW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BXH', 'UAAH', NULL, 'Балхаш', 'BXH', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('DMB', 'UADD', 'ДМБ', 'Аулие-Ата', 'DMB', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KGF', 'UAKK', 'КГД', 'Сары-Арка', 'KGF', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KZO', 'UAOO', 'КЗО', 'Кзыл-Орда', 'KZO', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KSN', 'UAUU', NULL, 'Наримановка', 'KSN', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('PPK', 'UACP', 'ПРА', 'Петропавловск', 'PPK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('PLX', 'UASS', 'СПЛ', 'Семей', 'PLX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('UKK', 'UASK', 'УГК', 'Усть-Каменогорск', 'UKK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('CIT', 'UAII', NULL, 'Шимкент', 'CIT', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('OSS', 'UCFO', 'ОШШ', 'Ош', 'OSS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KIV', 'LUKK', NULL, 'Кишинев', 'KIV', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('TJU', 'UTDK', NULL, 'Куляб', 'TJU', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('LBD', 'UTDL', NULL, 'Худжанд', 'LBD', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('TAS', 'UTTT', NULL, 'Ташкент', 'TAS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('TBS', 'UGTB', NULL, 'Тбилиси', 'TBS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SUI', 'UGSS', NULL, 'Бабушери', 'SUI', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KCK', 'UIKK', 'КРН', 'Киренск', 'KCK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ERG', 'UIKE', 'ЕГЧ', 'Ербогачен', 'ERG', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('EYK', 'USHQ', 'БЛР', 'Белоярский', 'BCX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BVJ', 'USDB', 'БОВ', 'Бованенково', 'BVJ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SBT', 'USDA', NULL, 'Сабетта', 'SBT', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('TQL', 'USDS', 'ТКС', 'Тарко-Сале', 'TQL', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ADH', 'UEEA', 'АЛД', 'Алдан', 'ADH', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('VYI', 'UENW', 'ВИК', 'Вилюйск', 'VYI', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ZKP', 'UESU', 'ЗНК', 'Зырянка', 'ZKP', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('GYG', 'UEMM', 'МГН', 'Маган', 'GYG', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('MQJ', 'UEMA', 'МОМ', 'Мома', 'MQJ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SUK', 'UEBS', 'СКЫ', 'Саккырыр', 'SUK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SYS', 'UERS', 'СЫХ', 'Саскылах', 'SYS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SUY', 'UENS', 'СУН', 'Сунтар', 'SUY', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('TLK', 'UECT', 'ТЛК', 'Талакан', 'TLK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('UMS', 'UEMU', 'УСМ', 'Усть-Мая', 'UMS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KDY', 'UEMH', 'ХДЫ', 'Теплый Ключ', 'KDY', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BXY', 'UAOL', 'ЛНЙ', 'Крайний', 'BXY', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('OSF', 'UUMO', 'ОСФ', 'Оста́фьево', 'MOW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('CKL', 'UUMU', NULL, 'Чкаловский', 'CKL', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KPW', 'UHMK', 'КПМ', 'Кепервеем', 'KPW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KVM', 'UHMO', 'МКО', 'Марково', 'KVM', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KPX', 'UHEK', 'КУП', 'Купол', 'KPX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('TGK', 'URRT', 'ТАГ', 'Таганрог-Южный', 'TGK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('EIK', 'URKE', 'ЕСК', 'Ейск', 'WZD', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BQG', 'UHNB', 'БГР', 'Богородское', 'BQG', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NGK', 'UHSN', 'НГЛ', 'Ноглики', 'NGK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('EKS', 'UHSK', 'ШАХ', 'Шахтерск', 'EKS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('AAQ', 'URKA', 'АНА', 'Витязево', 'AAQ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('JOK', 'UWKJ', 'ИШО', 'Йошкар-Ола', 'JOK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KHV', 'UHHH', 'ХБР', 'Хабаровск-Новый', 'KHV', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KUF', 'UWWW', 'СКЧ', 'Курумоч', 'KUF', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KYZ', 'UNKY', 'КЫЫ', 'Кызыл', 'KYZ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('LNX', 'UUBS', 'СМЛ', 'Смоленск-Южный', 'LNX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('MMK', 'ULMM', 'МУН', 'Мурманск', 'MMK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NJC', 'USNN', 'НЖВ', 'Нижневартовск', 'NJC', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NUX', 'USMU', 'НУР', 'Новый Уренгой', 'NUX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('OHO', 'UHOO', 'ОХТ', 'Охотск', 'OHO', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('OVB', 'UNNT', 'ТЛЧ', 'Толмачёво', 'OVB', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('PEZ', 'UWPP', 'ПНА', 'Пенза', 'PEZ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('PWE', 'UHMP', 'ПЕВ', 'Певек', 'PWE', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('RTW', 'UWSS', 'СРО', 'Саратов-Центральный', 'RTW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SGC', 'USRR', 'СУР', 'Сургут', 'SGC', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('TBW', 'UUOT', 'ТМБ', 'Донское', 'TBW', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('TYD', 'UHBW', 'ТЫД', 'Тында', 'TYD', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ULV', 'UWLL', 'УЛК', 'Баратаевка', 'ULY', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('UUD', 'UIUU', 'УЛЭ', 'Байкал', 'UUD', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('VKT', 'UUYW', 'ВКТ', 'Воркута', 'VKT', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('VUS', 'ULWU', 'ВЕУ', 'Великий Устюг', 'VUS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NER', 'UELL', 'НРГ', 'Чульман', 'NER', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NLI', 'UHNN', 'НЛК', 'Николаевск-на-Амуре', 'NLI', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('PYJ', 'UERP', 'ПЛЯ', 'Полярный', 'PYJ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SIP', 'URFF', 'СИП', 'Симферополь', 'SIP', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ALA', 'UAAA', 'АЛА', 'Алматы', 'ALA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('DPT', 'UEWD', 'ДЕП', 'Депутатский', 'DPT', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('GYD', 'UBBB', 'БАК', 'Гейдар Алиев', 'BAK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ONK', 'UERO', 'ОЛН', 'Оленек', 'ONK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ZIX', 'UEVV', 'ЖИГ', 'Жиганск', 'ZIX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('EIE', 'UNII', 'ЕНС', 'Енисейск', 'EIE', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NEF', 'UWUF', NULL, 'Нефтекамск', 'NEF', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('PVS', 'UHMD', 'ПРД', 'Бухта Провидения', 'PVS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('UIK', 'UIBS', 'УТК', 'Усть-Ильимск', 'UIK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NAJ', 'UBBN', NULL, 'Нахичевань', 'NAJ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('GME', 'UMGG', NULL, 'Гомель', 'GME', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('AKX', 'UATT', NULL, 'Актюбинск', 'AKX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BXJ', 'UAAR', NULL, 'Бурундай', 'BXJ', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('KOV', 'UACK', 'КЧТ', 'Кокшетау', 'KOV', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('URA', 'UARR', 'УРЛ', 'Уральск', 'URA', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('BZY', 'LUBL', NULL, 'Бельцы-Лядовены', 'BZY', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('DYU', 'UTDD', NULL, 'Душанбе', 'DYU', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ODO', 'UIKB', 'БДБ', 'Бодайбо', 'ODO', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('THX', 'UOTT', 'ТНХ', 'Туруханск', 'THX', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('OVS', 'USHS', 'СОЙ', 'Советский', 'OVS', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('VHV', 'UENI', 'ВХВ', 'Верхневилюйск', 'VHV', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('NYR', 'UENN', 'НЮР', 'Нюрба́', 'NYR', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SEK', 'UESK', 'СРМ', 'Среднеколымск', 'SEK', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('ULY', 'UWLW', 'УЛС', 'Ульяновск (Восточный)', 'ULY', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('SWV', 'UHMW', 'СВЕ', 'Северо-Эвенск', 'SWV', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('IGT', 'URMS', 'ИНШ', 'Магас', 'IGT', 55.972778, 37.414722);
INSERT INTO "PSP".dict_airports VALUES ('DEE', 'UHSM', 'ЮЖК', 'Менделеево', 'DEE', 55.972778, 37.414722);


--
-- TOC entry 4971 (class 0 OID 16791)
-- Dependencies: 222
-- Data for Name: dict_cities; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".dict_cities VALUES ('ABA', 'Абакан');
INSERT INTO "PSP".dict_cities VALUES ('DYR', 'Анадырь');
INSERT INTO "PSP".dict_cities VALUES ('AAQ', 'Анапа');
INSERT INTO "PSP".dict_cities VALUES ('WZA', 'Апатиты');
INSERT INTO "PSP".dict_cities VALUES ('ARH', 'Архангельск');
INSERT INTO "PSP".dict_cities VALUES ('ASF', 'Астрахань');
INSERT INTO "PSP".dict_cities VALUES ('BWO', 'Балаково');
INSERT INTO "PSP".dict_cities VALUES ('BAX', 'Барнаул');
INSERT INTO "PSP".dict_cities VALUES ('EGO', 'Белгород');
INSERT INTO "PSP".dict_cities VALUES ('BCX', 'Белоярский');
INSERT INTO "PSP".dict_cities VALUES ('WZC', 'Березники');
INSERT INTO "PSP".dict_cities VALUES ('BQS', 'Благовещенск');
INSERT INTO "PSP".dict_cities VALUES ('BTK', 'Братск');
INSERT INTO "PSP".dict_cities VALUES ('UUA', 'Бугульма');
INSERT INTO "PSP".dict_cities VALUES ('BKA', 'Быково');
INSERT INTO "PSP".dict_cities VALUES ('VLU', 'Великие Луки');
INSERT INTO "PSP".dict_cities VALUES ('VUS', 'Великий Устюг');
INSERT INTO "PSP".dict_cities VALUES ('GNO', 'Великий Новгород');
INSERT INTO "PSP".dict_cities VALUES ('VVO', 'Владивосток');
INSERT INTO "PSP".dict_cities VALUES ('OGZ', 'Владикавказ');
INSERT INTO "PSP".dict_cities VALUES ('VOG', 'Волгоград');
INSERT INTO "PSP".dict_cities VALUES ('VLK', 'Волгодонск');
INSERT INTO "PSP".dict_cities VALUES ('VGD', 'Вологда');
INSERT INTO "PSP".dict_cities VALUES ('VKT', 'Воркута');
INSERT INTO "PSP".dict_cities VALUES ('VOZ', 'Воронеж');
INSERT INTO "PSP".dict_cities VALUES ('GDZ', 'Геленджик');
INSERT INTO "PSP".dict_cities VALUES ('GRV', 'Грозный');
INSERT INTO "PSP".dict_cities VALUES ('WZD', 'Ейск');
INSERT INTO "PSP".dict_cities VALUES ('SVX', 'Екатеринбург');
INSERT INTO "PSP".dict_cities VALUES ('IWA', 'Иваново');
INSERT INTO "PSP".dict_cities VALUES ('IJK', 'Ижевск');
INSERT INTO "PSP".dict_cities VALUES ('INA', 'Инта');
INSERT INTO "PSP".dict_cities VALUES ('IKT', 'Иркутск');
INSERT INTO "PSP".dict_cities VALUES ('JOK', 'Йошкар-Ола');
INSERT INTO "PSP".dict_cities VALUES ('KZN', 'Казань');
INSERT INTO "PSP".dict_cities VALUES ('KGD', 'Калининград');
INSERT INTO "PSP".dict_cities VALUES ('KEJ', 'Кемерово');
INSERT INTO "PSP".dict_cities VALUES ('KVX', 'Киров');
INSERT INTO "PSP".dict_cities VALUES ('KVK', 'Кировск');
INSERT INTO "PSP".dict_cities VALUES ('KGP', 'Когалым');
INSERT INTO "PSP".dict_cities VALUES ('WZH', 'Колхи');
INSERT INTO "PSP".dict_cities VALUES ('KXK', 'Комсомольск-на-Амуре');
INSERT INTO "PSP".dict_cities VALUES ('KMW', 'Кострома');
INSERT INTO "PSP".dict_cities VALUES ('KSZ', 'Котлас');
INSERT INTO "PSP".dict_cities VALUES ('WZI', 'Крайний');
INSERT INTO "PSP".dict_cities VALUES ('KRR', 'Краснодар');
INSERT INTO "PSP".dict_cities VALUES ('KJA', 'Красноярск');
INSERT INTO "PSP".dict_cities VALUES ('KRO', 'Курган');
INSERT INTO "PSP".dict_cities VALUES ('KUR', 'Курск');
INSERT INTO "PSP".dict_cities VALUES ('KYZ', 'Кызыл');
INSERT INTO "PSP".dict_cities VALUES ('LPK', 'Липецк');
INSERT INTO "PSP".dict_cities VALUES ('GDX', 'Магадан');
INSERT INTO "PSP".dict_cities VALUES ('MQF', 'Магнитогорск');
INSERT INTO "PSP".dict_cities VALUES ('WZJ', 'Майкоп');
INSERT INTO "PSP".dict_cities VALUES ('MCX', 'Махачкала');
INSERT INTO "PSP".dict_cities VALUES ('MRV', 'Минеральные Воды');
INSERT INTO "PSP".dict_cities VALUES ('MJZ', 'Мирный');
INSERT INTO "PSP".dict_cities VALUES ('MOW', 'Москва');
INSERT INTO "PSP".dict_cities VALUES ('MMK', 'Мурманск');
INSERT INTO "PSP".dict_cities VALUES ('NYM', 'Надым');
INSERT INTO "PSP".dict_cities VALUES ('IGT', 'Магас');
INSERT INTO "PSP".dict_cities VALUES ('NAL', 'Нальчик');
INSERT INTO "PSP".dict_cities VALUES ('NNM', 'Нарьян-Мар');
INSERT INTO "PSP".dict_cities VALUES ('NER', 'Нерюнгри');
INSERT INTO "PSP".dict_cities VALUES ('NFG', 'Нефтеюганск');
INSERT INTO "PSP".dict_cities VALUES ('NJC', 'Нижневартовск');
INSERT INTO "PSP".dict_cities VALUES ('NBC', 'Нижнекамск');
INSERT INTO "PSP".dict_cities VALUES ('GOJ', 'Нижний Новгород');
INSERT INTO "PSP".dict_cities VALUES ('NOZ', 'Новокузнецк');
INSERT INTO "PSP".dict_cities VALUES ('OVB', 'Новосибирск');
INSERT INTO "PSP".dict_cities VALUES ('NUX', 'Новый Уренгой');
INSERT INTO "PSP".dict_cities VALUES ('NSK', 'Норильск');
INSERT INTO "PSP".dict_cities VALUES ('NOJ', 'Ноябрьск');
INSERT INTO "PSP".dict_cities VALUES ('OKT', 'Октябрьский');
INSERT INTO "PSP".dict_cities VALUES ('OMS', 'Омск');
INSERT INTO "PSP".dict_cities VALUES ('OEL', 'Орел');
INSERT INTO "PSP".dict_cities VALUES ('REN', 'Оренбург');
INSERT INTO "PSP".dict_cities VALUES ('OSW', 'Орск');
INSERT INTO "PSP".dict_cities VALUES ('OHO', 'Охотск');
INSERT INTO "PSP".dict_cities VALUES ('PWE', 'Певек');
INSERT INTO "PSP".dict_cities VALUES ('PEZ', 'Пенза');
INSERT INTO "PSP".dict_cities VALUES ('PEE', 'Пермь');
INSERT INTO "PSP".dict_cities VALUES ('PES', 'Петрозаводск');
INSERT INTO "PSP".dict_cities VALUES ('PKC', 'Петропавловск-Камчатский');
INSERT INTO "PSP".dict_cities VALUES ('PEX', 'Печора');
INSERT INTO "PSP".dict_cities VALUES ('PYJ', 'Полярный');
INSERT INTO "PSP".dict_cities VALUES ('PKV', 'Псков');
INSERT INTO "PSP".dict_cities VALUES ('PTG', 'Пятигорск');
INSERT INTO "PSP".dict_cities VALUES ('RAT', 'Радужный');
INSERT INTO "PSP".dict_cities VALUES ('ROV', 'Ростов-на-Дону');
INSERT INTO "PSP".dict_cities VALUES ('RYB', 'Рыбинск');
INSERT INTO "PSP".dict_cities VALUES ('RZN', 'Рязань');
INSERT INTO "PSP".dict_cities VALUES ('SLY', 'Салехард');
INSERT INTO "PSP".dict_cities VALUES ('KUF', 'Самара');
INSERT INTO "PSP".dict_cities VALUES ('LED', 'Санкт-Петербург');
INSERT INTO "PSP".dict_cities VALUES ('SKX', 'Саранск');
INSERT INTO "PSP".dict_cities VALUES ('RTW', 'Саратов');
INSERT INTO "PSP".dict_cities VALUES ('SIP', 'Симферополь');
INSERT INTO "PSP".dict_cities VALUES ('WZN', 'Слепцовская');
INSERT INTO "PSP".dict_cities VALUES ('LNX', 'Смоленск');
INSERT INTO "PSP".dict_cities VALUES ('WZO', 'Сокол');
INSERT INTO "PSP".dict_cities VALUES ('AER', 'Сочи');
INSERT INTO "PSP".dict_cities VALUES ('STW', 'Ставрополь');
INSERT INTO "PSP".dict_cities VALUES ('WZP', 'Старыйоскол');
INSERT INTO "PSP".dict_cities VALUES ('SWT', 'Стрежевой');
INSERT INTO "PSP".dict_cities VALUES ('SUZ', 'Суздаль');
INSERT INTO "PSP".dict_cities VALUES ('SGC', 'Сургут');
INSERT INTO "PSP".dict_cities VALUES ('SCW', 'Сыктывкар');
INSERT INTO "PSP".dict_cities VALUES ('TBW', 'Тамбов');
INSERT INTO "PSP".dict_cities VALUES ('TVE', 'Тверь');
INSERT INTO "PSP".dict_cities VALUES ('IKS', 'Тикси');
INSERT INTO "PSP".dict_cities VALUES ('TOX', 'Тобольск');
INSERT INTO "PSP".dict_cities VALUES ('TOF', 'Томск');
INSERT INTO "PSP".dict_cities VALUES ('TYD', 'Тында');
INSERT INTO "PSP".dict_cities VALUES ('TJM', 'Тюмень');
INSERT INTO "PSP".dict_cities VALUES ('UUD', 'Улан-Удэ');
INSERT INTO "PSP".dict_cities VALUES ('ULY', 'Ульяновск');
INSERT INTO "PSP".dict_cities VALUES ('USK', 'Усинск');
INSERT INTO "PSP".dict_cities VALUES ('UIK', 'Усть-Ильимск');
INSERT INTO "PSP".dict_cities VALUES ('UKX', 'Усть-Кут');
INSERT INTO "PSP".dict_cities VALUES ('UFA', 'Уфа');
INSERT INTO "PSP".dict_cities VALUES ('UCT', 'Ухта');
INSERT INTO "PSP".dict_cities VALUES ('KHV', 'Хабаровск');
INSERT INTO "PSP".dict_cities VALUES ('WZE', 'Ханты-Мансийск');
INSERT INTO "PSP".dict_cities VALUES ('WZT', 'Хибины');
INSERT INTO "PSP".dict_cities VALUES ('CSY', 'Чебоксары');
INSERT INTO "PSP".dict_cities VALUES ('CEK', 'Челябинск');
INSERT INTO "PSP".dict_cities VALUES ('CEE', 'Череповец');
INSERT INTO "PSP".dict_cities VALUES ('HTA', 'Чита');
INSERT INTO "PSP".dict_cities VALUES ('ESL', 'Элиста');
INSERT INTO "PSP".dict_cities VALUES ('UUS', 'Южно-Сахалинск');
INSERT INTO "PSP".dict_cities VALUES ('YKS', 'Якутск');
INSERT INTO "PSP".dict_cities VALUES ('IAR', 'Ярославль');
INSERT INTO "PSP".dict_cities VALUES ('RGK', 'Горно-Алтайск');
INSERT INTO "PSP".dict_cities VALUES ('ZIA', 'Жуковский');
INSERT INTO "PSP".dict_cities VALUES ('OLZ', 'Олекминск');
INSERT INTO "PSP".dict_cities VALUES ('ULK', 'Ленск');
INSERT INTO "PSP".dict_cities VALUES ('NLI', 'Николаевск-на-Амуре');
INSERT INTO "PSP".dict_cities VALUES ('OHH', 'Оха');
INSERT INTO "PSP".dict_cities VALUES ('BZK', 'Брянск');
INSERT INTO "PSP".dict_cities VALUES ('KLF', 'Калуга');
INSERT INTO "PSP".dict_cities VALUES ('ALA', 'Алматы');
INSERT INTO "PSP".dict_cities VALUES ('BGN', 'Белая Гора');
INSERT INTO "PSP".dict_cities VALUES ('BQJ', 'Батагай');
INSERT INTO "PSP".dict_cities VALUES ('CKH', 'Чокурдах');
INSERT INTO "PSP".dict_cities VALUES ('DPT', 'Депутатский');
INSERT INTO "PSP".dict_cities VALUES ('EVN', 'Ереван');
INSERT INTO "PSP".dict_cities VALUES ('FRU', 'Бишкек');
INSERT INTO "PSP".dict_cities VALUES ('BAK', 'Баку');
INSERT INTO "PSP".dict_cities VALUES ('IAA', 'Игарка');
INSERT INTO "PSP".dict_cities VALUES ('MSQ', 'Минск');
INSERT INTO "PSP".dict_cities VALUES ('TSE', 'Нурсултан');
INSERT INTO "PSP".dict_cities VALUES ('ONK', 'Оленек');
INSERT INTO "PSP".dict_cities VALUES ('SEK', 'Среднеколымск');
INSERT INTO "PSP".dict_cities VALUES ('UKG', 'Усть-Куйга');
INSERT INTO "PSP".dict_cities VALUES ('USR', 'Усть-Нера');
INSERT INTO "PSP".dict_cities VALUES ('ZIX', 'Жиганск');
INSERT INTO "PSP".dict_cities VALUES ('ADH', 'Алдан');
INSERT INTO "PSP".dict_cities VALUES ('AMV', 'Амдерма');
INSERT INTO "PSP".dict_cities VALUES ('ACS', 'Ачинск');
INSERT INTO "PSP".dict_cities VALUES ('DKS', 'Диксон');
INSERT INTO "PSP".dict_cities VALUES ('EIE', 'Енисейск');
INSERT INTO "PSP".dict_cities VALUES ('KVR', 'Кавалерово');
INSERT INTO "PSP".dict_cities VALUES ('LDG', 'Лешуконское');
INSERT INTO "PSP".dict_cities VALUES ('NEF', 'Нефтекамск');
INSERT INTO "PSP".dict_cities VALUES ('NYA', 'Нягань');
INSERT INTO "PSP".dict_cities VALUES ('PVS', 'Провидения');
INSERT INTO "PSP".dict_cities VALUES ('SEV', 'Северодонецк');
INSERT INTO "PSP".dict_cities VALUES ('CSH', 'Соловецкий');
INSERT INTO "PSP".dict_cities VALUES ('TYA', 'Тула');
INSERT INTO "PSP".dict_cities VALUES ('URJ', 'Урай');
INSERT INTO "PSP".dict_cities VALUES ('HTG', 'Хатанга');
INSERT INTO "PSP".dict_cities VALUES ('CYX', 'Черский');
INSERT INTO "PSP".dict_cities VALUES ('KVD', 'Гянджа');
INSERT INTO "PSP".dict_cities VALUES ('NAJ', 'Нахичевань');
INSERT INTO "PSP".dict_cities VALUES ('LWN', 'Гюмри');
INSERT INTO "PSP".dict_cities VALUES ('BQT', 'Брест');
INSERT INTO "PSP".dict_cities VALUES ('VTB', 'Витебск');
INSERT INTO "PSP".dict_cities VALUES ('GME', 'Гомель');
INSERT INTO "PSP".dict_cities VALUES ('GNA', 'Гродно');
INSERT INTO "PSP".dict_cities VALUES ('MVQ', 'Могилев');
INSERT INTO "PSP".dict_cities VALUES ('SCO', 'Актау');
INSERT INTO "PSP".dict_cities VALUES ('AKX', 'Актюбинск');
INSERT INTO "PSP".dict_cities VALUES ('GUW', 'Атырау');
INSERT INTO "PSP".dict_cities VALUES ('BXH', 'Балхаш');
INSERT INTO "PSP".dict_cities VALUES ('BXJ', 'Бурундай');
INSERT INTO "PSP".dict_cities VALUES ('DMB', 'Тараз');
INSERT INTO "PSP".dict_cities VALUES ('KGF', 'Караганда');
INSERT INTO "PSP".dict_cities VALUES ('KZO', 'Кзыл-Орда');
INSERT INTO "PSP".dict_cities VALUES ('KOV', 'Кокшетау');
INSERT INTO "PSP".dict_cities VALUES ('KSN', 'Костанай');
INSERT INTO "PSP".dict_cities VALUES ('PWQ', 'Павлодар');
INSERT INTO "PSP".dict_cities VALUES ('PPK', 'Петропавловск');
INSERT INTO "PSP".dict_cities VALUES ('PLX', 'Семипалатинск');
INSERT INTO "PSP".dict_cities VALUES ('URA', 'Уральск');
INSERT INTO "PSP".dict_cities VALUES ('UKK', 'Усть-Каменогорск');
INSERT INTO "PSP".dict_cities VALUES ('CIT', 'Шимкент');
INSERT INTO "PSP".dict_cities VALUES ('OSS', 'Ош');
INSERT INTO "PSP".dict_cities VALUES ('BZY', 'Бельцы');
INSERT INTO "PSP".dict_cities VALUES ('KIV', 'Кишинев');
INSERT INTO "PSP".dict_cities VALUES ('TJU', 'Куляб');
INSERT INTO "PSP".dict_cities VALUES ('LBD', 'Худжанд');
INSERT INTO "PSP".dict_cities VALUES ('DYU', 'Душанбе');
INSERT INTO "PSP".dict_cities VALUES ('TAS', 'Ташкент');
INSERT INTO "PSP".dict_cities VALUES ('TBS', 'Тбилиси');
INSERT INTO "PSP".dict_cities VALUES ('SUI', 'Сухуми');
INSERT INTO "PSP".dict_cities VALUES ('ODO', 'Бодайбо');
INSERT INTO "PSP".dict_cities VALUES ('KCK', 'Киренск');
INSERT INTO "PSP".dict_cities VALUES ('ERG', 'Ербогачен');
INSERT INTO "PSP".dict_cities VALUES ('THX', 'Туруханск');
INSERT INTO "PSP".dict_cities VALUES ('BVJ', 'Бованенково');
INSERT INTO "PSP".dict_cities VALUES ('SBT', 'Сабетта');
INSERT INTO "PSP".dict_cities VALUES ('OVS', 'Советский');
INSERT INTO "PSP".dict_cities VALUES ('TQL', 'Тарко-Сале');
INSERT INTO "PSP".dict_cities VALUES ('VHV', 'Верхневилюйск');
INSERT INTO "PSP".dict_cities VALUES ('VYI', 'Вилюйск');
INSERT INTO "PSP".dict_cities VALUES ('ZKP', 'Зырянка');
INSERT INTO "PSP".dict_cities VALUES ('GYG', 'Маган');
INSERT INTO "PSP".dict_cities VALUES ('MQJ', 'Хонуу');
INSERT INTO "PSP".dict_cities VALUES ('NYR', 'Нюрба́');
INSERT INTO "PSP".dict_cities VALUES ('SUK', 'Батагай-Алыта');
INSERT INTO "PSP".dict_cities VALUES ('SYS', 'Саскылах');
INSERT INTO "PSP".dict_cities VALUES ('SUY', 'Сунтар');
INSERT INTO "PSP".dict_cities VALUES ('TLK', 'Талакан');
INSERT INTO "PSP".dict_cities VALUES ('UMS', 'Усть-Мая');
INSERT INTO "PSP".dict_cities VALUES ('KDY', 'Теплый Ключ');
INSERT INTO "PSP".dict_cities VALUES ('BXY', 'Байконур');
INSERT INTO "PSP".dict_cities VALUES ('CKL', 'Чкаловский');
INSERT INTO "PSP".dict_cities VALUES ('KPW', 'Кепервеем');
INSERT INTO "PSP".dict_cities VALUES ('KVM', 'Марково');
INSERT INTO "PSP".dict_cities VALUES ('SWV', 'Эвенск');
INSERT INTO "PSP".dict_cities VALUES ('KPX', 'Купол');
INSERT INTO "PSP".dict_cities VALUES ('TGK', 'Таганрог');
INSERT INTO "PSP".dict_cities VALUES ('BQG', 'Богородское');
INSERT INTO "PSP".dict_cities VALUES ('NGK', 'Ноглики');
INSERT INTO "PSP".dict_cities VALUES ('DEE', 'Южно-Курильск');
INSERT INTO "PSP".dict_cities VALUES ('EKS', 'Шахтерск');


--
-- TOC entry 4972 (class 0 OID 16798)
-- Dependencies: 223
-- Data for Name: dict_document_types; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".dict_document_types VALUES ('00', 'Паспорт гражданина Российской Федерации');
INSERT INTO "PSP".dict_document_types VALUES ('01', 'Удостоверение личности моряка (паспорт моряка)');
INSERT INTO "PSP".dict_document_types VALUES ('02', 'Общегражданский заграничный паспорт гражданина Российской Федерации');
INSERT INTO "PSP".dict_document_types VALUES ('04', 'Свидетельство о рождении');
INSERT INTO "PSP".dict_document_types VALUES ('05', 'Удостоверение личности военнослужащего');
INSERT INTO "PSP".dict_document_types VALUES ('07', 'Временное удостоверение личности, выдаваемое органами внутренних дел');
INSERT INTO "PSP".dict_document_types VALUES ('08', 'Военный билет военнослужащего или курсанта военной образовательной организации');
INSERT INTO "PSP".dict_document_types VALUES ('10', 'Справка об освобождении из мест лишения свободы');
INSERT INTO "PSP".dict_document_types VALUES ('11', 'Паспорт гражданина СССР');
INSERT INTO "PSP".dict_document_types VALUES ('12', 'Паспорт дипломатический');
INSERT INTO "PSP".dict_document_types VALUES ('13', 'Паспорт служебный (кроме паспорта моряка и дипломатического)');
INSERT INTO "PSP".dict_document_types VALUES ('14', 'Свидетельство о возвращении из стран СНГ');
INSERT INTO "PSP".dict_document_types VALUES ('15', 'Справка об утере паспорта');
INSERT INTO "PSP".dict_document_types VALUES ('20', 'Документ, удостоверяющий личность осуждённого к принудительным работам');
INSERT INTO "PSP".dict_document_types VALUES ('99', 'Другие документы, установленные федеральным законодательством или признаваемые в соответствии с международными договорами РФ в качестве документов, удостоверяющих личность пассажира');
INSERT INTO "PSP".dict_document_types VALUES ('66', 'test');
INSERT INTO "PSP".dict_document_types VALUES ('21', 'string');
INSERT INTO "PSP".dict_document_types VALUES ('23', 'string');


--
-- TOC entry 4981 (class 0 OID 17202)
-- Dependencies: 232
-- Data for Name: dict_fare; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".dict_fare VALUES ('5NI', 76000, 'RUB', true);
INSERT INTO "PSP".dict_fare VALUES ('5NR', 76000, 'RUB', true);


--
-- TOC entry 4974 (class 0 OID 16810)
-- Dependencies: 225
-- Data for Name: dict_genders; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".dict_genders VALUES ('M', 'мужской');
INSERT INTO "PSP".dict_genders VALUES ('F', 'женский');


--
-- TOC entry 4980 (class 0 OID 17184)
-- Dependencies: 231
-- Data for Name: dict_operation_type; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".dict_operation_type VALUES ('issued', 'оффрмленая квота');
INSERT INTO "PSP".dict_operation_type VALUES ('refund', 'возвращенная квота');
INSERT INTO "PSP".dict_operation_type VALUES ('used', 'использованная квота');


--
-- TOC entry 4975 (class 0 OID 16817)
-- Dependencies: 226
-- Data for Name: dict_passenger_types; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".dict_passenger_types VALUES ('child', 'ребенок', 'Сопровождаемый ребенок до 12 лет с предоставлением места', '{1,3,4}', '{age}');
INSERT INTO "PSP".dict_passenger_types VALUES ('youth', 'молодежь', 'Гражданин в возрасте до 23 лет', '{1,3,4}', '{age}');
INSERT INTO "PSP".dict_passenger_types VALUES ('elderly', 'пенсионер', 'Мужчина от 60 лет, Женщина от 55 лет', '{1,3,4}', '{age}');
INSERT INTO "PSP".dict_passenger_types VALUES ('invalid_1', 'инвалид I группы', 'Инвалид I группы от 18 лет', '{1,3,4}', '{invalid}');
INSERT INTO "PSP".dict_passenger_types VALUES ('invalid_23', 'инвалид с детства II или III группы', 'Инвалид с детства II или III группы от 18 лет', '{1,3,4}', '{invalid}');
INSERT INTO "PSP".dict_passenger_types VALUES ('invalid_child', 'ребенок-инвалид до 18 лет', 'Ребенок-инвалид в возрасте до 18 лет', '{1,3,4}', '{invalid}');
INSERT INTO "PSP".dict_passenger_types VALUES ('attendant_invalid_1', 'сопровождающий инвалида I группы', 'Сопровождающий инвалида I группы от 18 лет', '{1,3,4}', '{attendant_invalid_1}');
INSERT INTO "PSP".dict_passenger_types VALUES ('attendant_invalid_child', 'сопровождающий ребенка-инвалида', 'Сопровождающий ребенка-инвалида от 18 лет', '{1,3,4}', '{attendant_invalid_child}');
INSERT INTO "PSP".dict_passenger_types VALUES ('large', 'член многодетной семьи', 'Гражданин, имеющий удостоверение многодетной семьи или иные документы, подтверждающие статус многодетной семьи в порядке, установленном нормативными правовыми актами субъектов Российской Федерации', '{1,3,4}', '{large}');
INSERT INTO "PSP".dict_passenger_types VALUES ('resident_dfo', 'житель ДФО', 'Гражданин РФ, зарегистрированный по месту жительства на территории субъекта РФ, входящего в состав Дальневосточного федерального округа', '{5}', '{resident_dfo}');
INSERT INTO "PSP".dict_passenger_types VALUES ('test', 'ребенок', 'ребенок', '{1}', '{age}');
INSERT INTO "PSP".dict_passenger_types VALUES ('infant', 'младенец', 'Младенец в возрасте от 0 до 2 лет без предоставления места', '{1,3,4}', '{age}');
INSERT INTO "PSP".dict_passenger_types VALUES ('ocean', 'отдыхающий в "ВДЦ "Океан"', 'Гражданин РФ в возрасте до 18 лет, на имя которого на определенный период текущего года оформлена путевка в ФГБОУ "ВДЦ "Океан"', '{2}', '{age}');


--
-- TOC entry 4976 (class 0 OID 16826)
-- Dependencies: 227
-- Data for Name: dict_quota_categories; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".dict_quota_categories VALUES ('age', 'Возраст', '{1}', 4, 2);
INSERT INTO "PSP".dict_quota_categories VALUES ('invalid', 'Инвалидность', '{1}', 4, 2);
INSERT INTO "PSP".dict_quota_categories VALUES ('attendant_invalid_1', 'Сопровождение инвалидов 1 группы', '{1}', 4, 2);
INSERT INTO "PSP".dict_quota_categories VALUES ('attendant_invalid_child', 'Сопровождение детей-инвалидов', '{1}', 4, 2);
INSERT INTO "PSP".dict_quota_categories VALUES ('large', 'Член многодетной семьи', '{1}', 4, 2);
INSERT INTO "PSP".dict_quota_categories VALUES ('resident_dfo', 'Регистрация в ДФО', '{5}', 4, 2);


--
-- TOC entry 4977 (class 0 OID 16836)
-- Dependencies: 228
-- Data for Name: dict_subsidized_routes; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".dict_subsidized_routes VALUES (1, 'DYR', 'ZIA', 1, 1350000, 1350000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (2, 'DYR', 'MOW', 1, 900000, 1350000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (3, 'DYR', 'OVB', 1, 650000, 815000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (4, 'BQS', 'SVX', 1, 600000, 900000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (5, 'BQS', 'ZIA', 1, 795000, 795000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (6, 'BQS', 'IKT', 1, 200000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (7, 'BQS', 'MOW', 1, 640000, 795000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (8, 'BQS', 'OVB', 1, 450000, 565000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (9, 'BQS', 'PKC', 1, 300000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (10, 'BQS', 'LED', 1, 650000, 975000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (11, 'BQS', 'SIP', 1, 980000, 1470000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (12, 'BQS', 'AER', 1, 950000, 1425000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (13, 'VVO', 'SVX', 1, 640000, 960000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (14, 'VVO', 'ZIA', 1, 1110000, 1110000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (15, 'VVO', 'GDX', 1, 300000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (16, 'VVO', 'MRV', 1, 1010000, 1515000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (17, 'VVO', 'MOW', 1, 740000, 1110000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (18, 'VVO', 'OVB', 1, 590000, 725000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (19, 'VVO', 'LED', 1, 750000, 1125000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (20, 'VVO', 'AER', 1, 1050000, 1575000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (21, 'RGK', 'ZIA', 1, 630000, 630000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (22, 'RGK', 'MOW', 1, 600000, 630000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (23, 'IKT', 'NER', 1, 200000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (24, 'IKT', 'OLZ', 1, 200000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (25, 'IKT', 'PYJ', 1, 450000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (26, 'IKT', 'LED', 1, 630000, 810000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (27, 'IKT', 'AER', 1, 680000, 885000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (28, 'KXK', 'MOW', 1, 720000, 1080000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (29, 'KJA', 'BQS', 1, 550000, 825000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (30, 'KJA', 'NER', 1, 300000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (31, 'KJA', 'PYJ', 1, 200000, 230000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (32, 'KJA', 'KHV', 1, 600000, 900000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (33, 'KJA', 'YKS', 1, 550000, 650000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (34, 'KYZ', 'LED', 1, 880000, 880000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', '{SVX}');
INSERT INTO "PSP".dict_subsidized_routes VALUES (35, 'KYZ', 'ZIA', 1, 800000, 800000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (36, 'KYZ', 'MOW', 1, 610000, 800000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (37, 'ULK', 'BTK', 1, 150000, 225000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (38, 'ULK', 'IKT', 1, 150000, 225000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (39, 'ULK', 'KJA', 1, 200000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (40, 'GDX', 'SVX', 1, 750000, 1125000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (41, 'GDX', 'ZIA', 1, 720000, 720000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (42, 'GDX', 'IKT', 1, 400000, 600000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (43, 'GDX', 'KRR', 1, 1000000, 1500000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (44, 'GDX', 'MOW', 1, 720000, 720000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (45, 'GDX', 'OVB', 1, 650000, 745000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (46, 'GDX', 'LED', 1, 720000, 1080000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (47, 'GDX', 'AER', 1, 1000000, 1500000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (48, 'GDX', 'KHV', 1, 400000, 600000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (49, 'MJZ', 'AAQ', 1, 1000000, 1500000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (50, 'MJZ', 'BTK', 1, 150000, 225000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (51, 'MJZ', 'GDZ', 1, 750000, 1125000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (52, 'MJZ', 'SVX', 1, 600000, 900000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (53, 'MJZ', 'ZIA', 1, 900000, 900000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (54, 'MJZ', 'IKT', 1, 200000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (55, 'MJZ', 'KJA', 1, 200000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (56, 'MJZ', 'KRR', 1, 700000, 1050000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (57, 'MJZ', 'MOW', 1, 710000, 900000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (58, 'MJZ', 'OVB', 1, 450000, 610000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (59, 'MJZ', 'LED', 1, 800000, 1200000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (60, 'MJZ', 'SIP', 1, 750000, 1125000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (61, 'MJZ', 'AER', 1, 750000, 1125000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (62, 'MJZ', 'TOF', 1, 250000, 375000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (63, 'MJZ', 'UUD', 1, 200000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (64, 'MJZ', 'HTA', 1, 150000, 225000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (65, 'NNM', 'ZIA', 1, 600000, 600000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (66, 'NNM', 'MOW', 1, 400000, 600000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (67, 'NER', 'ZIA', 1, 900000, 900000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (68, 'NER', 'MOW', 1, 710000, 900000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (69, 'NER', 'OVB', 1, 600000, 720000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (70, 'NER', 'SIP', 1, 800000, 1200000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (71, 'NLI', 'OHH', 1, 150000, 225000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (72, 'NSK', 'AAQ', 1, 680000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (73, 'NSK', 'GDZ', 1, 680000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (74, 'NSK', 'ZIA', 1, 645000, 645000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (75, 'NSK', 'SVX', 1, 300000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (76, 'NSK', 'KRR', 1, 550000, 825000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (77, 'NSK', 'MRV', 1, 670000, 900000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (78, 'NSK', 'MOW', 1, 430000, 645000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (79, 'NSK', 'OVB', 1, 250000, 375000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (80, 'NSK', 'ROV', 1, 500000, 750000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (81, 'NSK', 'LED', 1, 430000, 645000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (82, 'NSK', 'SIP', 1, 680000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (83, 'NSK', 'AER', 1, 680000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (84, 'NSK', 'UFA', 1, 350000, 525000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (85, 'PWE', 'ZIA', 1, 1250000, 1250000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (86, 'PWE', 'MOW', 1, 900000, 1250000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (87, 'PKC', 'VVO', 1, 600000, 900000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (88, 'PKC', 'MOW', 1, 750000, 1125000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (89, 'PKC', 'OVB', 1, 620000, 790000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (90, 'PKC', 'LED', 1, 750000, 1125000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (91, 'PKC', 'KHV', 1, 250000, 375000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (92, 'PYJ', 'AAQ', 1, 700000, 1050000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (93, 'PYJ', 'GDZ', 1, 700000, 1050000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (94, 'PYJ', 'ZIA', 1, 1065000, 1065000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (95, 'PYJ', 'KRR', 1, 700000, 1050000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (96, 'PYJ', 'MOW', 1, 710000, 1065000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (97, 'PYJ', 'OVB', 1, 600000, 800000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (98, 'PYJ', 'SIP', 1, 700000, 1050000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (99, 'PYJ', 'AER', 1, 700000, 1050000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (100, 'PEE', 'SVX', 1, 100000, 150000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (101, 'PEE', 'NBC', 1, 100000, 150000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (102, 'PEE', 'TJM', 1, 100000, 150000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (103, 'PEE', 'CEK', 1, 100000, 150000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (104, 'LED', 'EGO', 1, 200000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (105, 'LED', 'SKX', 1, 150000, 225000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (106, 'LED', 'CSY', 1, 150000, 225000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (107, 'SCW', 'KZN', 1, 100000, 150000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (108, 'IKS', 'ZIA', 1, 1200000, 1200000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (109, 'IKS', 'MOW', 1, 800000, 1200000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (110, 'UUD', 'ZIA', 1, 755000, 755000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (111, 'UUD', 'SVX', 1, 550000, 550000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (112, 'UUD', 'MOW', 1, 620000, 755000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (113, 'UUD', 'OVB', 1, 250000, 375000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (114, 'UUD', 'LED', 1, 850000, 850000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (115, 'UUD', 'AER', 1, 885000, 885000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (116, 'USK', 'PEE', 1, 150000, 225000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (117, 'KHV', 'VVO', 1, 180000, 215000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (118, 'KHV', 'GDZ', 1, 800000, 1200000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (119, 'KHV', 'SVX', 1, 620000, 930000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (120, 'KHV', 'ZIA', 1, 1070000, 1070000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (121, 'KHV', 'IKT', 1, 300000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (122, 'KHV', 'MRV', 1, 780000, 1170000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (123, 'KHV', 'MOW', 1, 720000, 1070000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (124, 'KHV', 'OVB', 1, 570000, 640000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (125, 'KHV', 'LED', 1, 730000, 1095000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (126, 'KHV', 'SIP', 1, 800000, 1200000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (127, 'KHV', 'AER', 1, 750000, 1125000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (128, 'HTA', 'SVX', 1, 500000, 750000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (129, 'HTA', 'ZIA', 1, 805000, 805000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (130, 'HTA', 'KRR', 1, 900000, 900000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (131, 'HTA', 'MOW', 1, 620000, 805000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (132, 'HTA', 'OVB', 1, 300000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (133, 'HTA', 'LED', 1, 870000, 870000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (134, 'HTA', 'AER', 1, 980000, 980000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (135, 'UUS', 'VVO', 1, 150000, 225000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (136, 'UUS', 'ZIA', 1, 1095000, 1095000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (137, 'UUS', 'MOW', 1, 730000, 1095000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (138, 'UUS', 'NLI', 1, 100000, 150000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (139, 'UUS', 'LED', 1, 740000, 1110000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (140, 'UUS', 'KHV', 1, 100000, 150000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (141, 'YKS', 'AAQ', 1, 840000, 1160000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (142, 'YKS', 'VVO', 1, 640000, 820000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (143, 'YKS', 'GDZ', 1, 780000, 1170000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (144, 'YKS', 'SVX', 1, 600000, 900000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (145, 'YKS', 'ZIA', 1, 1000000, 1000000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (146, 'YKS', 'IKT', 1, 250000, 375000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (147, 'YKS', 'KRR', 1, 850000, 1275000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (148, 'YKS', 'GDX', 1, 150000, 225000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (149, 'YKS', 'MRV', 1, 760000, 1140000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (150, 'YKS', 'MOW', 1, 700000, 1000000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (151, 'YKS', 'OVB', 1, 550000, 645000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (152, 'YKS', 'OMS', 1, 450000, 675000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (153, 'YKS', 'LED', 1, 700000, 1000000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (154, 'YKS', 'SIP', 1, 800000, 1200000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (155, 'YKS', 'AER', 1, 780000, 1170000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (156, 'YKS', 'UFA', 1, 845000, 845000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (157, 'YKS', 'UUS', 1, 250000, 375000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (158, 'GDX', 'VVO', 2, 620000, 620000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (159, 'YKS', 'VVO', 2, 640000, 640000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (160, 'UUS', 'VVO', 2, 600000, 600000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (161, 'HTA', 'VVO', 2, 510000, 510000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (162, 'IKT', 'VVO', 2, 550000, 550000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (163, 'KJA', 'VVO', 2, 570000, 570000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (164, 'OVB', 'VVO', 2, 590000, 590000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (165, 'SVX', 'VVO', 2, 640000, 640000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (166, 'SLY', 'VVO', 2, 890000, 890000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', '{SVX}');
INSERT INTO "PSP".dict_subsidized_routes VALUES (167, 'ABA', 'SIP', 3, 587500, 705000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (168, 'AAQ', 'SIP', 3, 185000, 222000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (169, 'ARH', 'SIP', 3, 375000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (170, 'ASF', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (171, 'BAX', 'SIP', 3, 625000, 750000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (172, 'EGO', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (173, 'BZK', 'SIP', 3, 337500, 405000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (174, 'UUA', 'SIP', 3, 350000, 420000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (175, 'OGZ', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (176, 'VOG', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (177, 'VOZ', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (178, 'GRV', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (279, 'SVX', 'SIP', 3, 375000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (280, 'IWA', 'SIP', 3, 337500, 405000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (281, 'IJK', 'SIP', 3, 355000, 420000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (282, 'IKT', 'SIP', 3, 850000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (283, 'KZN', 'SIP', 3, 275000, 330000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (284, 'KLF', 'SIP', 3, 337500, 405000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (285, 'KEJ', 'SIP', 3, 625000, 750000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (286, 'KVX', 'SIP', 3, 355000, 426000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (287, 'KMW', 'SIP', 3, 337500, 405000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (288, 'KRR', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (289, 'KJA', 'SIP', 3, 700000, 840000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (290, 'KRO', 'SIP', 3, 387500, 465000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (291, 'KUR', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (292, 'LPK', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (293, 'GDX', 'SIP', 3, 1250000, 1500000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (294, 'MQF', 'SIP', 3, 500000, 600000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (295, 'MCX', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (296, 'MRV', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (297, 'MMK', 'SIP', 3, 375000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (298, 'NAL', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (299, 'NJC', 'SIP', 3, 550000, 660000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (300, 'NBC', 'SIP', 3, 375000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (301, 'GOJ', 'SIP', 3, 337500, 405000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (302, 'NOZ', 'SIP', 3, 600000, 720000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (303, 'OVB', 'SIP', 3, 600000, 720000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (304, 'OMS', 'SIP', 3, 500000, 600000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (305, 'REN', 'SIP', 3, 337500, 405000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (306, 'OSW', 'SIP', 3, 337500, 405000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (307, 'PEZ', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (308, 'PEE', 'SIP', 3, 350000, 420000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (309, 'PES', 'SIP', 3, 375000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (310, 'PKV', 'SIP', 3, 350000, 420000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (311, 'ROV', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (312, 'KUF', 'SIP', 3, 337500, 405000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (313, 'RTW', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (314, 'AER', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (315, 'STW', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (316, 'SGC', 'SIP', 3, 525000, 630000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (317, 'SCW', 'SIP', 3, 375000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (318, 'TBW', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (319, 'TOF', 'SIP', 3, 625000, 750000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (320, 'TJM', 'SIP', 3, 425000, 510000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (321, 'UUD', 'SIP', 3, 887500, 1065000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (322, 'ULY', 'SIP', 3, 337500, 405000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (323, 'UFA', 'SIP', 3, 312500, 375000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (324, 'WZE', 'SIP', 3, 687500, 825000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (325, 'CSY', 'SIP', 3, 337500, 405000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (326, 'CEK', 'SIP', 3, 375000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (327, 'CEE', 'SIP', 3, 300000, 360000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (328, 'HTA', 'SIP', 3, 887500, 1065000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (329, 'IAR', 'SIP', 3, 250000, 300000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (330, 'KGD', 'ARH', 4, 550000, 500000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (331, 'KGD', 'SVX', 4, 850000, 770000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (332, 'KGD', 'ZIA', 4, 380000, 380000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (333, 'KGD', 'KLF', 4, 380000, 350000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (334, 'KGD', 'MOW', 4, 380000, 550000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (335, 'KGD', 'MMK', 4, 600000, 270000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (336, 'KGD', 'LED', 4, 350000, 450000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (337, 'KGD', 'AER', 4, 650000, 590000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (338, 'GDX', 'MOW', 5, 1020000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (339, 'UUS', 'MOW', 5, 1020000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (340, 'KHV', 'MOW', 5, 1020000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (341, 'VVO', 'MOW', 5, 1020000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (342, 'PKC', 'MOW', 5, 1020000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (343, 'DYR', 'MOW', 5, 1020000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (344, 'BQS', 'MOW', 5, 970000, 970000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (345, 'MJZ', 'MOW', 5, 870000, 870000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (346, 'NER', 'MOW', 5, 970000, 970000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (347, 'PWE', 'MOW', 5, 970000, 2570000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (348, 'PYJ', 'MOW', 5, 870000, 870000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (349, 'UUD', 'MOW', 5, 920000, 920000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (350, 'HTA', 'MOW', 5, 920000, 920000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (351, 'YKS', 'MOW', 5, 920000, 920000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (352, 'BQS', 'LED', 5, 970000, 970000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (353, 'VVO', 'LED', 5, 1020000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (354, 'MJZ', 'LED', 5, 870000, 870000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (355, 'PKC', 'LED', 5, 1020000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (356, 'KHV', 'LED', 5, 1020000, 1020000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (357, 'HTA', 'LED', 5, 920000, 920000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);
INSERT INTO "PSP".dict_subsidized_routes VALUES (358, 'YKS', 'LED', 5, 920000, 920000, 'RUB', '2021-12-25 00:00:00', '3333-12-31 23:59:59', NULL);


--
-- TOC entry 4979 (class 0 OID 16847)
-- Dependencies: 230
-- Data for Name: dict_ticket_types; Type: TABLE DATA; Schema: PSP; Owner: -
--

INSERT INTO "PSP".dict_ticket_types VALUES (1, 'авиабилет');


--
-- TOC entry 5080 (class 0 OID 0)
-- Dependencies: 216
-- Name: data_coupon_events_id_seq; Type: SEQUENCE SET; Schema: PSP; Owner: -
--

SELECT pg_catalog.setval('"PSP".data_coupon_events_id_seq', 30, true);


--
-- TOC entry 5081 (class 0 OID 0)
-- Dependencies: 217
-- Name: data_coupon_events_passenger_id_seq; Type: SEQUENCE SET; Schema: PSP; Owner: -
--

SELECT pg_catalog.setval('"PSP".data_coupon_events_passenger_id_seq', 1, false);


--
-- TOC entry 5082 (class 0 OID 0)
-- Dependencies: 219
-- Name: data_passenger_id_seq; Type: SEQUENCE SET; Schema: PSP; Owner: -
--

SELECT pg_catalog.setval('"PSP".data_passenger_id_seq', 19, true);


--
-- TOC entry 5083 (class 0 OID 0)
-- Dependencies: 229
-- Name: dict_subsidized_routes_id_seq; Type: SEQUENCE SET; Schema: PSP; Owner: -
--

SELECT pg_catalog.setval('"PSP".dict_subsidized_routes_id_seq', 1, false);


--
-- TOC entry 4780 (class 2606 OID 17263)
-- Name: data_coupon_events data_coupon_events_pk; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_coupon_events
    ADD CONSTRAINT data_coupon_events_pk PRIMARY KEY (id);


--
-- TOC entry 4782 (class 2606 OID 16864)
-- Name: data_passenger data_passenger_pk; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_passenger
    ADD CONSTRAINT data_passenger_pk PRIMARY KEY (id);


--
-- TOC entry 4784 (class 2606 OID 16870)
-- Name: dict_airlines dict_airlines_pkey; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_airlines
    ADD CONSTRAINT dict_airlines_pkey PRIMARY KEY (iata_code);


--
-- TOC entry 4786 (class 2606 OID 16872)
-- Name: dict_airports dict_airports_pkey; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_airports
    ADD CONSTRAINT dict_airports_pkey PRIMARY KEY (iata_code);


--
-- TOC entry 4788 (class 2606 OID 16874)
-- Name: dict_cities dict_cities_pkey; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_cities
    ADD CONSTRAINT dict_cities_pkey PRIMARY KEY (iata_code);


--
-- TOC entry 4790 (class 2606 OID 16876)
-- Name: dict_document_types dict_document_types_pkey; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_document_types
    ADD CONSTRAINT dict_document_types_pkey PRIMARY KEY (code);


--
-- TOC entry 4806 (class 2606 OID 17209)
-- Name: dict_fare dict_fare_pk; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_fare
    ADD CONSTRAINT dict_fare_pk PRIMARY KEY (code);


--
-- TOC entry 4792 (class 2606 OID 16878)
-- Name: data_flight dict_flight_pk; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_flight
    ADD CONSTRAINT dict_flight_pk PRIMARY KEY (code);


--
-- TOC entry 4794 (class 2606 OID 16880)
-- Name: dict_genders dict_genders_pkey; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_genders
    ADD CONSTRAINT dict_genders_pkey PRIMARY KEY (code);


--
-- TOC entry 4804 (class 2606 OID 17190)
-- Name: dict_operation_type dict_operation_type_pk; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_operation_type
    ADD CONSTRAINT dict_operation_type_pk PRIMARY KEY (code);


--
-- TOC entry 4796 (class 2606 OID 16882)
-- Name: dict_passenger_types dict_passenger_types_pkey; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_passenger_types
    ADD CONSTRAINT dict_passenger_types_pkey PRIMARY KEY (code);


--
-- TOC entry 4798 (class 2606 OID 16884)
-- Name: dict_quota_categories dict_quota_categories_pkey; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_quota_categories
    ADD CONSTRAINT dict_quota_categories_pkey PRIMARY KEY (code);


--
-- TOC entry 4800 (class 2606 OID 16886)
-- Name: dict_subsidized_routes dict_subsidized_routes_pkey; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_subsidized_routes
    ADD CONSTRAINT dict_subsidized_routes_pkey PRIMARY KEY (id);


--
-- TOC entry 4802 (class 2606 OID 16888)
-- Name: dict_ticket_types dict_ticket_type_pkey; Type: CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_ticket_types
    ADD CONSTRAINT dict_ticket_type_pkey PRIMARY KEY (code);


--
-- TOC entry 4807 (class 2606 OID 17285)
-- Name: data_coupon_events data_coupon_events_document_type_fk; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_coupon_events
    ADD CONSTRAINT data_coupon_events_document_type_fk FOREIGN KEY (document_type_code) REFERENCES "PSP".dict_document_types(code);


--
-- TOC entry 4808 (class 2606 OID 17270)
-- Name: data_coupon_events data_coupon_events_flight_fk; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_coupon_events
    ADD CONSTRAINT data_coupon_events_flight_fk FOREIGN KEY (flight_code) REFERENCES "PSP".data_flight(code);


--
-- TOC entry 4809 (class 2606 OID 17280)
-- Name: data_coupon_events data_coupon_events_operation_type_fk; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_coupon_events
    ADD CONSTRAINT data_coupon_events_operation_type_fk FOREIGN KEY (operation_type) REFERENCES "PSP".dict_operation_type(code);


--
-- TOC entry 4810 (class 2606 OID 17244)
-- Name: data_coupon_events data_coupon_events_passenger_fk; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_coupon_events
    ADD CONSTRAINT data_coupon_events_passenger_fk FOREIGN KEY (passenger_id) REFERENCES "PSP".data_passenger(id);


--
-- TOC entry 4811 (class 2606 OID 17290)
-- Name: data_coupon_events data_coupon_events_quota_fk; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_coupon_events
    ADD CONSTRAINT data_coupon_events_quota_fk FOREIGN KEY (quota_code) REFERENCES "PSP".dict_quota_categories(code);


--
-- TOC entry 4812 (class 2606 OID 17275)
-- Name: data_coupon_events data_coupon_events_ticket_type_fk; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_coupon_events
    ADD CONSTRAINT data_coupon_events_ticket_type_fk FOREIGN KEY (ticket_type) REFERENCES "PSP".dict_ticket_types(code);


--
-- TOC entry 4815 (class 2606 OID 17295)
-- Name: data_flight data_flight_arrive_place_fk; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_flight
    ADD CONSTRAINT data_flight_arrive_place_fk FOREIGN KEY (arrive_place) REFERENCES "PSP".dict_airports(iata_code);


--
-- TOC entry 4813 (class 2606 OID 16914)
-- Name: data_passenger data_passenger_gender_fk; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_passenger
    ADD CONSTRAINT data_passenger_gender_fk FOREIGN KEY (gender) REFERENCES "PSP".dict_genders(code);


--
-- TOC entry 4814 (class 2606 OID 16944)
-- Name: dict_airports dict_airports_fkey_city_iata_code; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_airports
    ADD CONSTRAINT dict_airports_fkey_city_iata_code FOREIGN KEY (city_iata_code) REFERENCES "PSP".dict_cities(iata_code);


--
-- TOC entry 4816 (class 2606 OID 16959)
-- Name: data_flight dict_flight_airline_fk; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_flight
    ADD CONSTRAINT dict_flight_airline_fk FOREIGN KEY (airline_code) REFERENCES "PSP".dict_airlines(iata_code);


--
-- TOC entry 4817 (class 2606 OID 16949)
-- Name: data_flight dict_flight_depart_place_fk; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_flight
    ADD CONSTRAINT dict_flight_depart_place_fk FOREIGN KEY (depart_place) REFERENCES "PSP".dict_airports(iata_code);


--
-- TOC entry 4818 (class 2606 OID 17257)
-- Name: data_flight dict_flight_fk; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".data_flight
    ADD CONSTRAINT dict_flight_fk FOREIGN KEY (fare_code) REFERENCES "PSP".dict_fare(code);


--
-- TOC entry 4819 (class 2606 OID 16964)
-- Name: dict_subsidized_routes dict_subsidized_routes_fkey_city_finish_iata_code; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_subsidized_routes
    ADD CONSTRAINT dict_subsidized_routes_fkey_city_finish_iata_code FOREIGN KEY (city_finish_iata_code) REFERENCES "PSP".dict_cities(iata_code);


--
-- TOC entry 4820 (class 2606 OID 16969)
-- Name: dict_subsidized_routes dict_subsidized_routes_fkey_city_start_iata_code; Type: FK CONSTRAINT; Schema: PSP; Owner: -
--

ALTER TABLE ONLY "PSP".dict_subsidized_routes
    ADD CONSTRAINT dict_subsidized_routes_fkey_city_start_iata_code FOREIGN KEY (city_start_iata_code) REFERENCES "PSP".dict_cities(iata_code);


-- Completed on 2024-03-10 15:46:56

--
-- PostgreSQL database dump complete
--

